## Overview

The POC demonstrates how an attacker can abuse the `--processStart` argument in the [Squirrel framework](https://github.com/Squirrel/Squirrel.Windows) to run a malicious payload. <br />

The attack modifies the shortcut that points to `Update.exe`, forcing it to run the payload first, and then execute the real application with elevated privileges.
<br />

This can be done with any application that uses Squirrel (Postman, Whatsapp, Teams etc).
