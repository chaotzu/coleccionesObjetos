# 🚀 Cómo publicar el wiki en GitHub

El wiki de GitHub es un **repositorio Git aparte** (`<repo>.wiki.git`). Estos son los pasos:

## 1. Sube el código

```bash
cd colecciones-csharp
./personalizar.sh TU_USUARIO_REAL            # reemplaza el marcador TU-USUARIO en los enlaces
git init -b main
git add .
git commit -m "Código de ejemplo y apuntes de colecciones en C#"
git remote add origin https://github.com/TU_USUARIO_REAL/colecciones-csharp.git
git push -u origin main
```

## 2. Activa e inicializa el wiki

1. En GitHub: **Settings → Features → Wikis** (actívalo si no lo está).
2. Pestaña **Wiki → Create the first page**: guarda cualquier texto. Esto crea el repositorio del wiki.

## 3. Sube las páginas del wiki

```bash
git clone https://github.com/TU_USUARIO_REAL/colecciones-csharp.wiki.git
cp -r colecciones-csharp/wiki/* colecciones-csharp.wiki/
cd colecciones-csharp.wiki
git add .
git commit -m "Apuntes completos"
git push
```

> 🔁 La rama del wiki puede llamarse `master`; si `git push` falla, usa `git push origin HEAD`.

## 4. Revisa

- Debe aparecer el **banner** y el **índice** en *Home*, con la barra lateral (`_Sidebar`) y el pie (`_Footer`).
- Los diagramas ` ```mermaid ` se renderizan automáticamente en GitHub.
- Los bloques `> [!TIP]` se muestran como avisos de color.
- Si algún enlace a un ejemplo da 404, verifica que el usuario/repo sean los correctos y que la rama sea `main`.

## Notas

- Los nombres de archivo del wiki usan guiones: `02-Arreglos.md` se muestra como la página "02 Arreglos".
- Para editar desde la web, usa el botón **Edit** de cada página; para trabajo en equipo (alumnos), usa el repositorio del wiki como cualquier otro.
