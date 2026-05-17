Vrînceanu Sterică – Interfață grafică și integrarea aplicației
    PaintApp/Program.cs – a configurat punctul de pornire al aplicației și handler-ele globale pentru excepții.
    PaintApp/MainCanvasForm.Designer.cs – a construit interfața Windows Forms: toolbar, canvas, 
    butoane pentru unelte, selector de culoare, selector de grosime și dialogul de salvare.
    PaintApp/MainCanvasForm.cs – a implementat logica principală a ferestrei: selectarea uneltei 
    active, gestionarea evenimentelor MouseDown, MouseMove, MouseUp, redimensionarea canvas-ului 
    și actualizarea interfeței.
    PaintApp/DoubleBufferedPanel.cs – a implementat componenta pentru desenare fluentă, fără 
    flicker.
    A avut rolul de integrare între GUI, DrawingTools.dll și StateManager.dll.

Membrul 2 – Modulul de desen DrawingTools.dll
    DrawingTools/IDrawStrategy.cs – a definit interfața comună pentru toate instrumentele de 
    desen.
    DrawingTools/ShapeToolBase.cs – a implementat logica de bază reutilizabilă pentru formele geometrice, inclusiv normalizarea coordonatelor.
    DrawingTools/PencilTool.cs – a implementat desenarea liberă cu creionul.
    DrawingTools/LineTool.cs – a implementat desenarea liniilor.
    DrawingTools/RectangleTool.cs – a implementat desenarea dreptunghiurilor.
    DrawingTools/EllipseTool.cs – a implementat desenarea cercurilor/elipselor.
    A asigurat extensibilitatea modulului, astfel încât să poată fi adăugate ulterior unelte noi.

Membrul 3 – Gestionarea stării, salvare și robustețe
    StateManager/ICommand.cs – a definit contractul pentru comenzile anulabile.
    StateManager/ICanvasHost.cs – a definit interfața dintre managerul de stare și canvas.
    StateManager/DrawCommand.cs – a implementat comanda de desen, cu starea anterioară și starea 
    nouă a imaginii.
    StateManager/CommandManager.cs – a implementat stivele de Undo/Redo, execuția comenzilor și 
    curățarea istoricului.
    PaintApp/MainCanvasForm.cs – a implementat partea de salvare a imaginii, alegerea formatului
    și integrarea comenzilor de istoric în fluxul de desenare.
    PaintApp/Program.cs și PaintApp/MainCanvasForm.cs – a tratat excepțiile la nivel global și 
    local, pentru a preveni închiderea bruscă a aplicației.

Membrul 4 – Testare și documentație
    PaintApp.Tests/PaintApp.Tests.csproj – a configurat proiectul de testare MSTest pentru 
    integrarea cu Test Explorer din Visual Studio.
    PaintApp.Tests/DrawingToolsTests.cs – a scris testele automate pentru uneltele de desen.
    PaintApp.Tests/StateManagerTests.cs – a scris testele automate pentru DrawCommand și 
    CommandManager.
    PaintApp.Tests/TestBitmapFactory.cs – a implementat utilitare pentru generarea bitmap-urilor 
    folosite în teste.
    PaintApp.Tests/TestCanvasHost.cs – a implementat un canvas fals folosit pentru verificarea 
    mecanismului Undo/Redo.
    Documentatie/TestCases.md – a redactat lista cu peste 20 de cazuri de test.
    SRS_IEEE_PaintApp.md – a redactat documentul SRS după modelul IEEE.
    Documentatie/Diagrame_UML.md și fișierele .svg – a realizat diagramele UML și organizarea 
    documentației tehnice.
    README.md – a completat descrierea structurii proiectului și a funcționalităților implementate.