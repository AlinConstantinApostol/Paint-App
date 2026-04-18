### Membrul 1 - Șeful de echipă: Nucleul (Core Engine) și Interfața Grafică (GUI)

#### Responsabilități

- crearea interfeței Windows Forms;
- gestionarea canvas-ului și a zonei de desen;
- tratarea evenimentelor `MouseDown`, `MouseMove`, `MouseUp`;
- conectarea butoanelor și comenzilor din toolbar;
- integrarea modulelor dezvoltate de ceilalți colegi;
- definirea structurii generale a soluției și a fluxului aplicației.

#### Livrabil

- proiectul principal executabil;
- arhitectura de bază a aplicației;
- formularul principal și integrarea componentelor.

### Membrul 2 - Pachetul de Instrumente de Desen (`DrawingTools.dll`)

#### Responsabilități

- definirea interfeței comune pentru instrumente;
- implementarea logicii de desen pentru `Creion`, `Linie`, `Dreptunghi`, `Elipsă/Cerc`;
- asigurarea posibilității de adăugare ulterioară a altor instrumente;
- testarea corectitudinii geometrice a fiecărui instrument.

#### Livrabil

- `DrawingTools.dll`

### Membrul 3 - Pachetul de Editare și I/O (`ImageProcessing.dll`)

#### Responsabilități

- logica de salvare și eventual deschidere a imaginilor;
- funcții de procesare de bază, precum `Crop` și `Resize`;
- implementarea filtrelor simple, cum ar fi `Alb-Negru` și `Sepia`;
- definirea interfețelor prin care nucleul aplicației poate apela aceste funcții.

#### Livrabil

- `ImageProcessing.dll`

### Membrul 4 - Gestionarea Stării (`StateManager.dll`) și Testare

#### Responsabilități

- implementarea sistemului de `Undo/Redo`;
- definirea și administrarea comenzilor pentru istoric;
- realizarea testelor unitare pentru modulele principale;
- redactarea documentului Help/manual de utilizare;
- elaborarea a minimum 20 de cazuri de test pentru validarea funcțională și structurală.

#### Livrabil

- `StateManager.dll`
- proiectul de unit testing;
- manualul Help;
- setul de cazuri de test.

## 8. Avantajele acestei împărțiri

Această împărțire este eficientă deoarece:

- separă clar zonele de responsabilitate;
- reduce conflictele dintre membri în timpul dezvoltării;
- permite lucrul în paralel pe module diferite;
- respectă cerința de modularitate prin DLL-uri separate;
- lasă zona de testare și documentare în responsabilitatea unui membru dedicat, ceea ce crește calitatea proiectului final.