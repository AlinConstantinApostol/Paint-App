# Cazuri de test - Paint App

Documentul de mai jos contine peste 20 de cazuri de test pentru proiect, pentru a acoperi cerinta de testare si pentru a sustine evaluarea modulara a aplicatiei.

## Cazuri automate - proiectul `PaintApp.Tests`

Proiectul de testare a fost convertit la `MSTest`, astfel incat testele pot fi rulate direct din Visual Studio prin `Test Explorer` folosind comanda `Run All Tests`.

| ID | Tip | Descriere | Rezultat asteptat |
|---|---|---|---|
| T01 | Automat | `PencilTool_DisplayName_IsCreion` | Numele instrumentului este `Creion`. |
| T02 | Automat | `PencilTool_DrawsContinuously_IsTrue` | Creionul deseneaza continuu la `MouseMove`. |
| T03 | Automat | `PencilTool_Draw_ChangesBitmap` | Bitmap-ul este modificat dupa desenare. |
| T04 | Automat | `LineTool_DisplayName_IsLinie` | Numele instrumentului este `Linie`. |
| T05 | Automat | `LineTool_Draw_ChangesBitmap` | Desenarea unei linii produce pixeli colorati. |
| T06 | Automat | `LineTool_Draw_MarksEndRegion` | Zona punctului final al liniei contine pixeli desenati. |
| T07 | Automat | `RectangleTool_DisplayName_IsDreptunghi` | Numele instrumentului este `Dreptunghi`. |
| T08 | Automat | `RectangleTool_Draw_ChangesBitmap` | Desenarea dreptunghiului modifica imaginea. |
| T09 | Automat | `RectangleTool_Draw_NormalizesCoordinates` | Dreptunghiul se deseneaza corect si cand utilizatorul trage invers. |
| T10 | Automat | `RectangleTool_Draw_MarksBottomRightRegion` | Coltul opus al dreptunghiului este desenat. |
| T11 | Automat | `EllipseTool_DisplayName_IsCerc` | Numele instrumentului este `Cerc`. |
| T12 | Automat | `EllipseTool_Draw_ChangesBitmap` | Desenarea elipsei modifica bitmap-ul. |
| T13 | Automat | `EllipseTool_Draw_NormalizesCoordinates` | Elipsa este desenata corect la coordonate inverse. |
| T14 | Automat | `EllipseTool_Draw_MarksTopArcRegion` | Arcul superior al elipsei este vizibil. |
| T15 | Automat | `DrawCommand_Execute_AppliesNewState` | `Execute()` aplica starea noua pe canvas. |
| T16 | Automat | `DrawCommand_Undo_AppliesPreviousState` | `Undo()` restaureaza starea anterioara. |
| T17 | Automat | `DrawCommand_Execute_UsesClonedBitmapState` | Comanda foloseste clone interne, nu referinte mutate ulterior. |
| T18 | Automat | `DrawCommand_Constructor_NullCanvas_Throws` | Constructorul respinge un `targetCanvas` null. |
| T19 | Automat | `CommandManager_ExecuteCommand_EnablesUndo` | Dupa executie, `Undo` devine disponibil. |
| T20 | Automat | `CommandManager_ExecuteCommand_Null_Throws` | Managerul respinge comenzi null. |
| T21 | Automat | `CommandManager_Undo_EnablesRedo` | Dupa `Undo`, `Redo` devine disponibil. |
| T22 | Automat | `CommandManager_Redo_ReappliesNewestState` | `Redo` reaplica ultima stare anulata. |
| T23 | Automat | `CommandManager_ExecuteCommand_ClearsRedoStack` | Istoricul `Redo` este golit dupa o noua comanda. |
| T24 | Automat | `CommandManager_ClearHistory_DisablesUndoAndRedo` | Dupa golirea istoricului, `Undo/Redo` sunt indisponibile. |
| T25 | Automat | `CommandManager_MultipleUndo_RestoresOldestState` | Doua `Undo` restaureaza starea initiala. |
| T26 | Automat | `CommandManager_MultipleRedo_ReappliesLatestState` | Doua `Redo` readuc ultima stare disponibila. |

## Cazuri manuale recomandate - interfata si exceptii

| ID | Tip | Descriere | Rezultat asteptat |
|---|---|---|---|
| M01 | Manual | Utilizatorul deseneaza cu `Creion` pe canvas. | Linia urmareste miscarea mouse-ului fara erori. |
| M02 | Manual | Utilizatorul deseneaza cu `Linie`, `Dreptunghi` si `Cerc`. | Formele sunt randate corect pe canvas. |
| M03 | Manual | Utilizatorul schimba culoarea si grosimea liniei. | Noile desene folosesc setarile selectate. |
| M04 | Manual | Utilizatorul salveaza imaginea intr-o locatie valida. | Aplicatia afiseaza mesaj de succes si fisierul este creat. |
| M05 | Manual | Utilizatorul incearca salvarea intr-o locatie fara drepturi. | Aplicatia afiseaza mesaj de eroare controlat, fara crash. |
| M06 | Manual | Utilizatorul foloseste `Undo` si `Redo` dupa mai multe desene. | Starile sunt restaurate in ordinea corecta. |

## Observatii

- Cazurile automate sunt implementate in proiectul `PaintApp.Tests` folosind atributele `[TestClass]` si `[TestMethod]`.
- Cazurile manuale pot fi incluse in documentatia finala pentru partea de GUI si tratare a exceptiilor.
- Totalul depaseste cerinta minima de 20 de cazuri de test.
