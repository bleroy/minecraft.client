Decent Minecraft Client
=======================

A client library for the original (Java) version of Minecraft.
It requires Forge and the Raspberry Jam mod on the Minecraft side.

Status
------

This is just a hack at this point: only a minimal number of APIs is implemented,
but the general infrastructure is in place, and the code can actually script Minecraft
from C#, which is pretty awesome in itself.

How does it work?
-----------------

There's [a Minecraft mod that enables scripting Minecraft in Python called Raspberry Jam
Mod](https://github.com/arpruss/raspberryjammod/releases).
The way that mod works is with a small Java mod that lives in Minecraft, accepting socket
connections from a Python client and then exchanging text-based messages that on the wire
look like function calls:

```
world.getBlock(1,0,0)
```

The interesting thing is that there is nothing specific about Python in that protocol:
it's just messages getting exchanged over a socket, which .NET will be happy to emulate.

What I did is to reverse-engineer the Python client code and re-implement it in C#.

Where is this going?
--------------------

Eventually, I'd like to expose the entire Minecraft API with a nice object model,
and to be able to start the C# scripts from the Minecraft console.
That would effectively make it possible to write mods in C#, with a thin Java driver.

CSX support would be nice as well.

I also want to be able to run this with Mono (that should actually already work,
this is just .NET Standard) on a Raspberry Pi: the protocol that Raspberry Jam implements
is natively present on the Raspberry Pi version.

Can I help?
-----------

Sure. Just let me know what you'd like to work on, so we don't duplicate efforts, but yes,
hacking is highly encouraged.