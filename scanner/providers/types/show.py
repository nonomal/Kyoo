import os
from dataclasses import asdict, dataclass, field
from datetime import date
from typing import Optional
from enum import Enum

from .genre import Genre
from .studio import Studio
from .season import Season
from .metadataid import MetadataID

class Status(str, Enum):
	UNKNOWN = "unknown"
	FINISHED = "finished"
	AIRING = "airing"
	PLANNED = "planned"


@dataclass
class ShowTranslation:
	name: str
	tagline: Optional[str]
	keywords: list[str]
	overview: Optional[str]

	posters: list[str]
	logos: list[str]
	trailers: list[str]
	thumbnails: list[str]


@dataclass
class Show:
	original_language: Optional[str]
	aliases: list[str]
	start_air: Optional[date | int]
	end_air: Optional[date | int]
	status: Status
	studios: list[Studio]
	genres: list[Genre]
	seasons: list[Season]
	# TODO: handle staff
	# staff: list[Staff]
	external_id: dict[str, MetadataID]

	translations: dict[str, ShowTranslation] = field(default_factory=dict)

	def format_date(self, date: date | int | None) -> str | None:
		if date is None:
			return None
		if isinstance(date, int):
			return f"{date}-01-01T00:00:00Z"
		return date.isoformat()

	def to_kyoo(self):
		# For now, the API of kyoo only support one language so we remove the others.
		default_language = os.environ["LIBRARY_LANGUAGES"].split(",")[0]
		return {
			**asdict(self),
			**asdict(self.translations[default_language]),
			"poster": next(iter(self.translations[default_language].posters), None),
			"thumbnail": next(
				iter(self.translations[default_language].thumbnails), None
			),
			"logo": next(iter(self.translations[default_language].logos), None),
			"trailer": next(iter(self.translations[default_language].trailers), None),
			"studio": next(iter(x.to_kyoo() for x in self.studios), None),
			"startAir": self.format_date(self.start_air),
			"endAir": self.format_date(self.end_air),
			"title": self.translations[default_language].name,
			"genres": [x.to_kyoo() for x in self.genres],
		}