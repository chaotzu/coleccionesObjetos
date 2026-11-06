#!/usr/bin/env bash
# Reemplaza el marcador TU-USUARIO por tu usuario de GitHub en todos los archivos.
# Uso:  ./personalizar.sh mi-usuario [nombre-del-repo]
set -euo pipefail

USUARIO="${1:?Uso: ./personalizar.sh <usuario-github> [nombre-repo]}"
REPO="${2:-colecciones-csharp}"

find . -type f \( -name "*.md" -o -name "*.cs" -o -name "*.svg" \) -not -path "./.git/*" -print0 |
while IFS= read -r -d '' archivo; do
  # Compatible con GNU sed (Linux/Git Bash) y BSD sed (macOS)
  sed -i.bak "s#github.com/TU-USUARIO/colecciones-csharp#github.com/${USUARIO}/${REPO}#g" "$archivo"
  rm -f "${archivo}.bak"
done

echo "Listo: enlaces apuntan a https://github.com/${USUARIO}/${REPO}"
