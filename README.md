## Overview

The POC demonstrates how an attacker can abuse the `--processStart` argument in the [Squirrel framework](https://github.com/Squirrel/Squirrel.Windows) to run a malicious payload, in this case, by targeting Discord. The attack modifies the shortcut that points to `Update.exe`, forcing it to run the payload first, and then execute Discord with elevated privileges.
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

### Result
<p align="center">
  <img src="https://user-images.githubusercontent.com/26238419/235329222-8341f37c-41e7-44fc-b5fb-5ce14667d65e.png">
  <br />
  <img src="https://user-images.githubusercontent.com/26238419/235329224-fcc5396c-b2fa-4c4b-b5e4-38a88836207d.png">
</p>
