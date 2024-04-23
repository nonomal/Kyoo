from logging import getLogger
from autosync.services.service import Service
from ..models.user import User
from ..models.show import Show
from ..models.movie import Movie
from ..models.episode import Episode
from ..models.watch_status import WatchStatus

logger = getLogger(__name__)


class Aggregate(Service):
	def __init__(self, services: list[Service]):
		self._services = [x for x in services if x.enabled]
		logger.info("Autosync enabled with %s", [x.name for x in self._services])

	@property
	def name(self) -> str:
		return "aggragate"

	def update(self, user: User, resource: Movie | Show | Episode, status: WatchStatus):
		for service in self._services:
			try:
				service.update(user, resource, status)
			except Exception as e:
				logger.exception(
					"Unhandled error on autosync %s:", service.name, exc_info=e
				)
