{pkgs ? import <nixpkgs> {}}: let
  python = pkgs.python311.withPackages (ps:
    with ps; [
      guessit
      aiohttp
      jsons
      watchfiles
    ]);
  dotnet = with pkgs.dotnetCorePackages;
    combinePackages [
      sdk_7_0
      aspnetcore_7_0
      aspnetcore_6_0
    ];
in
  pkgs.mkShell {
    packages = with pkgs; [
      nodejs-18_x
      nodePackages.yarn
      nodePackages.eas-cli
      nodePackages.expo-cli
      dotnet
      csharpier
      python
      ruff
      go
      wgo
      mediainfo
      libmediainfo
      ffmpeg-full
      postgresql_15
      eslint_d
      prettierd
      pgformatter
      websocat
    ];

    DOTNET_ROOT = "${dotnet}";
  }
