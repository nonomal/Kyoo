#!/usr/bin/env python3

if __name__ == "__main__":
	import sys
	from pathlib import Path

	sys.path.append(str(Path(f"{__file__}/../../..").resolve()))

from guessit.api import default_api
from typing import cast, List
from rebulk import Rebulk

try:
	from . import rules
except:
	import rules

default_api.configure({})
rblk = cast(Rebulk, default_api.rebulk)
rblk.rules(rules)


def guessit(name: str, *, xem_titles: List[str] = []):
	return default_api.guessit(
		name,
		{
			"episode_prefer_number": True,
			"excludes": "language",
			"xem_titles": xem_titles,
		},
	)


# Only used to test localy
if __name__ == "__main__":
	import sys
	import json
	from providers.implementations.thexem import TheXemClient
	from guessit.jsonutils import GuessitEncoder
	from aiohttp import ClientSession
	import asyncio

	async def main():
		async with ClientSession() as client:
			xem = TheXemClient(client)

			ret = guessit(sys.argv[1], xem_titles=await xem.get_expected_titles())
			print(json.dumps(ret, cls=GuessitEncoder, indent=4))

	asyncio.run(main())
