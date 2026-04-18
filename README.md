# Aplicatie Paint - Proiect IP

Aplicatia este organizata in 3 proiecte WinForms/.NET:

- `PaintApp` - interfata grafica principala si canvas-ul.
- `DrawingTools` - strategiile de desen (`Creion`, `Linie`, `Dreptunghi`, `Cerc`).
- `StateManager` - istoricul actiunilor prin `CommandManager` si `DrawCommand`.

Functionalitati implementate:

- desenare cu mouse-ul pe canvas;
- selectie instrument din toolbar;
- salvare imagine pe disc;
- `Undo/Redo` pentru fiecare actiune de desen;
- arhitectura extensibila pentru instrumente noi.
