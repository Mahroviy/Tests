Tantei
=================


[![.NET build](https://github.com/TanteiBot/Tantei/actions/workflows/build.yml/badge.svg)](https://github.com/TanteiBot/Tantei/actions/workflows/build.yml) ![GitHub](https://img.shields.io/github/license/TanteiBot/Tantei?label=License&style=flat-square)  
Tantei is Discord bot that tracks user's updates from various anime/manga list websites such as [MyAnimeList](https://myanimelist.net), [Shikimori](https://shikimori.one), [AniList](https://anilist.co) and posts them to Discord server/servers.

Installation
---------------------
Prerequisites: git, .NET SDK 7.0
- `git clone --branch v2 https://github.com/TanteiBot/Tantei.git`
- `cd Tantei/`
- `dotnet publish -c Release -o publish/ PaperMalKing/PaperMalKing.csproj`
- `cd publish/`
- `cp template.appsettings.json appsettings.json`
- Fill necessary data in `appsettings.json`

Run with `dotnet PaperMalKing.dll`

Notice
---------------------
Project is unofficial and is not affiliated with MyAnimeList.net, Shikimori.one, AniList.co or any other website/application from which project can get users updates.

License
---------------------

Copyright 2021-2022 N0D4N

Licensed under the AGPLv3: https://www.gnu.org/licenses/agpl-3.0.html

[![License](https://www.gnu.org/graphics/agplv3-with-text-100x42.png)](https://www.gnu.org/licenses/agpl-3.0.html)
