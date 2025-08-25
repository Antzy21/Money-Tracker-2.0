# Dev Container Setup

[Info on Dev containers](https://code.visualstudio.com/docs/devcontainers/containers?originUrl=%2Fdocs%2Fdevcontainers%2Fcreate-dev-container)

[Info on using a Dockerfile as the base image](https://containers.dev/guide/dockerfile)

---
The Dockerfile in this directory is used as the base image for the dev container.

The base is a dotnet image.  
Then it installs node and ef-core.

The `postCreateScript.sh` is run in the `postCreateCommand` section.  
It installs npm packages and sets up the ef database.

### Issues

Sometimes C# dev kit doesn't work when first (re)building the dev container.  
Restarting VS Code fixed this. It doesn't rebuild the container, just restarts it. The C# Extension errors are still visible in the terminal, but the intellisense works.