# Oaklands modding docs

The source of the Oaklands modding docs site. Everything here is generated
from the game's code and copied in automatically, so changes made here
directly are overwritten. To fix something, report it to the Oaklands team.

- `docs/`: the pages, and the example mods under `docs/examples`.
- `mkdocs.yml`: the site's settings ([MkDocs](https://www.mkdocs.org) with
  [Material for MkDocs](https://squidfunk.github.io/mkdocs-material/)).
- `.github/workflows/deploy.yml`: builds the site and publishes it to GitHub
  Pages on every push.

To preview it locally:

```bash
pip install -r requirements.txt
mkdocs serve
```
