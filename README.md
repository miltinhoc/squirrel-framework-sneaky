This repository contains a proof of concept (POC) leveraging the [Squirrel framework](https://github.com/Squirrel/Squirrel.Windows) to trick users into escalating privileges on Windows systems.

## Overview

The POC demonstrates how an attacker can abuse the `--processStart` argument in the Squirrel framework to run a malicious payload, in this case, by targeting Discord. The attack modifies the shortcut that points to `Update.exe`, forcing it to run the payload first, and then execute Discord with elevated privileges.
<br />

This can be done with any application that uses Squirrel (postman, whatsapp, etc).

## Example

We modify the Discord shortcut to call `Update.exe` with the following arguments:

```bash
Update.exe --processStart "Payload.exe" --process-start-args "Discord.exe"
```

This results in the following execution flow:

1. The `Update.exe` is called with the `--processStart` argument pointing to `Payload.exe`.
2. `Payload.exe` is executed with elevated privileges by `Update.exe`.
3. `Payload.exe` then calls `Discord.exe`.

