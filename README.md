DepotDownloader with depot key support
===============

Steam depot downloader utilizing the SteamKit2 library. Supports .NET 5.0

### Downloading one depot for an app using a depot key
```
dotnet DepotDownloader.dll -app <id> -depot <id> [-manifest <id>]]
    -depot-key <key>
```

For example: `dotnet DepotDownloader.dll -app 220 -depot 221 -depot-key f5e105...`

## Parameters

Parameter | Description
--------- | -----------
-app \<#>				| the AppID to download.
-depot \<#>				| the DepotID to download.
-depot-key \<key>       | the depot key to use in hex.
-manifest \<id>			| manifest id of content to download (requires -depot, default: current for branch).
-ugc \<#>				| the UGC ID to download.
-beta \<branchname>		| download from specified branch if available (default: Public).
-betapassword \<pass>	| branch password if applicable.
-all-platforms			| downloads all platform-specific depots when -app is used.
-os \<os>				| the operating system for which to download the game (windows, macos or linux, default: OS the program is currently running on)
-osarch \<arch>			| the architecture for which to download the game (32 or 64, default: the host's architecture)
-all-languages			| download all language-specific depots when -app is used.
-language \<lang>		| the language for which to download the game (default: english)
-lowviolence			| download low violence depots when -app is used.
-pubfile \<#>			| the PublishedFileId to download. (Will automatically resolve to UGC id)
-username \<user>		| the username of the account to login to for restricted content.
-password \<pass>		| the password of the account to login to for restricted content.
-remember-password		| if set, remember the password for subsequent logins of this user.
-dir \<installdir>		| the directory in which to place downloaded files.
-filelist \<file.txt>	| a list of files to download (from the manifest). Prefix file path with `regex:` if you want to match with regex.
-validate				| Include checksum verification of files already downloaded
-manifest-only			| downloads a human readable manifest for any depots that would be downloaded.
-cellid \<#>			| the overridden CellID of the content server to download from.
-max-servers \<#>		| maximum number of content servers to use. (default: 20).
-max-downloads \<#>		| maximum number of chunks to download concurrently. (default: 8).
-loginid \<#>			| a unique 32-bit integer Steam LogonID in decimal, required if running multiple instances of DepotDownloader concurrently.
