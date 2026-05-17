# Software Requirements Specification (SRS)

## Aplicație Paint pentru Windows Forms

Versiune document: 1.0  
Data: 18.04.2026

## 1. Introducere

### 1.1 Scop

Prezentul document descrie cerințele software pentru aplicația desktop de tip Paint, dezvoltată în C# folosind Windows Forms. Sistemul permite utilizatorului să deseneze pe un canvas folosind mouse-ul, să selecteze instrumente grafice de bază și să salveze imaginea rezultată pe disc.

Documentul este redactat după structura generală a modelului IEEE SRS și are rolul de a servi drept referință pentru analiză, proiectare, implementare, testare și evaluare.

### 1.2 Domeniu de aplicare

Aplicația propusă este un editor grafic simplificat, inspirat de Microsoft Paint, destinat utilizatorilor care au nevoie de funcții de desen 2D de bază. Produsul va funcționa pe sistemul de operare Windows și va oferi o interfață intuitivă pentru desenare, editare minimală și salvare.

Sistemul este proiectat modular, astfel încât noi instrumente sau funcționalități să poată fi adăugate cu efort redus, prin DLL-uri separate.

### 1.3 Definiții, acronime și abrevieri

- `SRS` - Software Requirements Specification
- `GUI` - Graphical User Interface
- `DLL` - Dynamic Link Library
- `Canvas` - suprafața de desen a aplicației
- `Undo/Redo` - mecanism de anulare și refacere a acțiunilor
- `Strategy Pattern` - șablon de proiectare folosit pentru instrumente de desen interschimbabile
- `Command Pattern` - șablon de proiectare folosit pentru gestionarea istoricului acțiunilor

### 1.4 Referințe

- Cerințele proiectului
- Standardele și recomandările generale IEEE pentru documente SRS
- Documentația Microsoft pentru Windows Forms și .NET

### 1.5 Structura documentului

Documentul este împărțit în următoarele secțiuni:

- descriere generală a produsului;
- cerințe funcționale și nefuncționale;
- interfețe externe;
- constrângeri de proiectare;
- scenarii de utilizare;
- împărțirea muncii într-o echipă de 4 membri.

## 2. Descriere generală

### 2.1 Perspectiva produsului

Aplicația Paint este un produs software standalone, cu interfață desktop, care rulează local pe calculatorul utilizatorului. Sistemul are o arhitectură modulară, cu separare între:

- nucleul aplicației și GUI;
- instrumentele de desen;
- managementul stării și istoricul acțiunilor;
- procesarea de imagini și operațiile I/O, ca modul extensibil.

Această separare urmărește cerința de modularitate și permite dezvoltarea și testarea independentă a componentelor.

### 2.2 Funcțiile produsului

Aplicația va oferi următoarele funcții principale:

- desenare liberă cu mouse-ul pe canvas;
- trasare de linii, dreptunghiuri și cercuri/elipse;
- selectarea instrumentului activ din bara de instrumente;
- alegerea culorii și a grosimii liniei;
- salvarea imaginii curente pe disc;
- anularea și refacerea acțiunilor de desen;
- posibilitatea de extindere cu filtre și funcții suplimentare.

### 2.3 Clase de utilizatori

- `Utilizator obișnuit`: dorește să deseneze rapid forme simple și să salveze rezultatul.
- `Dezvoltator/maintainer`: adaugă instrumente noi, filtre sau alte funcționalități în structura modulară a aplicației.

### 2.4 Mediul de operare

- Sistem de operare: Windows 10 sau Windows 11
- Platformă software: .NET 8 sau versiune compatibilă
- IDE recomandat: Microsoft Visual Studio
- Tehnologie GUI: Windows Forms

### 2.5 Constrângeri de proiectare și implementare

- limbajul de programare utilizat este C#;
- interfața grafică va fi implementată în Windows Forms;
- proiectul trebuie organizat modular, prin DLL-uri separate;
- pentru instrumentele de desen se va utiliza șablonul `Strategy`;
- pentru istoricul acțiunilor se va utiliza șablonul `Command`;
- aplicația trebuie să poată fi compilată și rulată local în Visual Studio.

### 2.6 Documentație utilizator

Produsul va fi însoțit de:

- un fișier `README` pentru structură și rulare;
- un document Help/ghid scurt de utilizare;
- documentația de testare.

### 2.7 Ipoteze și dependențe

- utilizatorul dispune de mouse sau alt dispozitiv de indicare;
- sistemul are drepturi de scriere pe disc pentru salvarea imaginilor;
- mediul Visual Studio și SDK-ul .NET sunt instalate corect pentru dezvoltare.

## 3. Cerințe specifice

## 3.1 Cerințe funcționale

### REQ-1: Desenare pe canvas

Sistemul trebuie să permită desenarea pe canvas cu ajutorul mouse-ului.

#### Descriere

Utilizatorul apasă butonul stâng al mouse-ului și execută acțiuni de desen pe suprafața grafică.

#### Intrări

- evenimente `MouseDown`, `MouseMove`, `MouseUp`;
- poziția cursorului pe canvas.

#### Ieșiri

- actualizarea vizuală a imaginii curente.

#### Prioritate

Critică

### REQ-2: Bară de instrumente

Sistemul trebuie să ofere o bară de instrumente cu opțiuni pentru:

- Creion
- Linie
- Dreptunghi
- Cerc/Elipsă

#### Descriere

La selectarea unui instrument, acesta devine instrumentul activ și va fi folosit la următoarea operație de desen.

#### Prioritate

Critică

### REQ-3: Salvarea imaginii

Sistemul trebuie să permită salvarea imaginii curente pe disc.

#### Descriere

Utilizatorul selectează opțiunea de salvare, alege locația și formatul fișierului, iar aplicația persistă imaginea.

#### Prioritate

Critică

### REQ-4: Selectarea culorii

Sistemul trebuie să permită alegerea culorii utilizate pentru desen.

#### Prioritate

Înaltă

### REQ-5: Selectarea grosimii liniei

Sistemul trebuie să permită modificarea grosimii liniei pentru instrumentele de desen.

#### Prioritate

Înaltă

### REQ-6: Undo/Redo

Sistemul trebuie să permită anularea și refacerea ultimelor acțiuni de desen.

#### Descriere

Fiecare operație finalizată de desen trebuie stocată în istoric ca o comandă, astfel încât utilizatorul să poată reveni la starea anterioară sau reaplica o stare anulată.

#### Prioritate

Critică

### REQ-7: Extensibilitatea instrumentelor

Sistemul trebuie să permită adăugarea facilă de noi instrumente fără modificări majore în nucleul aplicației.

#### Descriere

Fiecare instrument nou trebuie să poată fi adăugat prin implementarea unei interfețe comune de desen și înregistrarea sa în GUI.

#### Prioritate

Înaltă

### REQ-8: Suport pentru procesare imagine

Sistemul ar trebui să permită integrarea viitoare a unor operații de procesare a imaginii, precum:

- Crop
- Resize
- Alb-Negru
- Sepia

#### Observație

Această cerință definește direcția de extensie modulară și poate fi implementată printr-un modul separat, de tip `ImageProcessing.dll`.

#### Prioritate

Medie

## 3.2 Cerințe privind interfețele externe

### 3.2.1 Interfața cu utilizatorul

GUI-ul va conține:

- o fereastră principală;
- un `ToolStrip` sau echivalent pentru instrumente și comenzi;
- un canvas central pentru desen;
- ferestre de dialog pentru alegerea culorii și salvarea fișierelor.

Interfața trebuie să fie clară, ușor de folosit și să reacționeze imediat la acțiunile utilizatorului.

### 3.2.2 Interfața hardware

Aplicația utilizează:

- mouse-ul pentru desen și selecție;
- tastatura pentru eventuale shortcut-uri viitoare;
- stocarea locală pentru salvarea fișierelor.

### 3.2.3 Interfața software

Sistemul va comunica intern între module prin interfețe și referințe de proiect:

- `DrawingTools.dll`
- `StateManager.dll`
- `ImageProcessing.dll` ca extensie planificată

### 3.2.4 Interfața de comunicare

Nu sunt necesare comunicații de rețea. Aplicația funcționează local.

## 3.3 Cerințe nefuncționale

### NFR-1: Extensibilitate

Arhitectura trebuie să permită adăugarea rapidă a unor noi instrumente sau funcții prin module separate, cu impact minim asupra codului existent.

### NFR-2: Modularitate

Codul aplicației trebuie împărțit în componente bine delimitate, compilate în DLL-uri distincte.

### NFR-3: Ușurință în utilizare

Interfața trebuie să fie intuitivă, astfel încât un utilizator fără instruire avansată să poată desena și salva o imagine.

### NFR-4: Performanță

Desenarea trebuie să se realizeze fluid, fără întârzieri perceptibile pentru operațiile standard pe canvas.

### NFR-5: Fiabilitate

Aplicația nu trebuie să piardă starea curentă în timpul operațiilor normale de lucru și trebuie să gestioneze elegant erorile de salvare.

### NFR-6: Mentenabilitate

Codul trebuie să fie clar, modular și documentat suficient pentru a permite extinderea sau refactorizarea ulterioară.

### NFR-7: Portabilitate limitată

Produsul este destinat platformei Windows. Nu se impune suport multi-platformă.

## 3.4 Cerințe logice privind baza de date

Nu se utilizează bază de date în această versiune a produsului.

## 3.5 Reguli de business și constrângeri logice

- desenarea este permisă doar în limitele canvas-ului;
- salvarea se poate efectua doar dacă există o imagine curentă validă;
- o acțiune de tip `Redo` este disponibilă doar după un `Undo`;
- selectarea unui nou instrument nu trebuie să șteargă desenul existent;
- adăugarea unui instrument nou trebuie să respecte contractul interfeței comune.

## 4. Scenarii de utilizare

### 4.1 Scenariul principal: desenarea unei forme

1. Utilizatorul pornește aplicația.
2. Sistemul afișează fereastra principală și canvas-ul.
3. Utilizatorul selectează un instrument, de exemplu `Dreptunghi`.
4. Utilizatorul apasă butonul mouse-ului pe canvas.
5. Utilizatorul deplasează cursorul.
6. Sistemul afișează forma desenată.
7. Utilizatorul eliberează butonul mouse-ului.
8. Sistemul finalizează forma și o adaugă în istoric.

### 4.2 Scenariul de salvare

1. Utilizatorul apasă butonul `Salvează`.
2. Sistemul deschide o fereastră de dialog pentru alegerea locației.
3. Utilizatorul selectează numele și formatul fișierului.
4. Sistemul salvează imaginea pe disc.
5. Sistemul afișează mesaj de confirmare sau eroare.

### 4.3 Scenariul de anulare

1. Utilizatorul desenează o formă.
2. Utilizatorul apasă `Undo`.
3. Sistemul revine la imaginea anterioară.
4. Utilizatorul apasă `Redo`.
5. Sistemul reaplică ultima modificare anulată.

## 5. Arhitectura software propusă

### 5.1 Modulul GUI și Core

Responsabil pentru:

- fereastra principală;
- canvas;
- evenimentele mouse-ului;
- selectarea instrumentului activ;
- integrarea cu modulele externe.

### 5.2 Modulul `DrawingTools.dll`

Responsabil pentru implementarea instrumentelor de desen folosind `Strategy Pattern`.

Componente exemplu:

- `IDrawStrategy`
- `PencilTool`
- `LineTool`
- `RectangleTool`
- `EllipseTool`

### 5.3 Modulul `StateManager.dll`

Responsabil pentru gestionarea istoricului prin `Command Pattern`.

Componente exemplu:

- `ICommand`
- `DrawCommand`
- `CommandManager`

### 5.4 Modulul `ImageProcessing.dll`

Modul planificat pentru operații suplimentare:

- deschidere/salvare avansată;
- crop;
- resize;
- filtre grafice.

## 6. Criterii de acceptanță

Produsul va fi considerat acceptat dacă:

- utilizatorul poate desena pe canvas cu toate instrumentele cerute;
- aplicația poate salva corect imaginea pe disc;
- mecanismul `Undo/Redo` funcționează pentru acțiunile de desen;
- soluția este organizată modular, în proiecte separate;
- aplicația se compilează și rulează fără erori în Visual Studio.


## 7. Concluzie

Aplicația Paint propusă respectă cerințele funcționale și nefuncționale impuse de proiect și poate fi dezvoltată într-o manieră clară, modulară și extensibilă. Structura bazată pe Windows Forms, `Strategy Pattern`, `Command Pattern` și DLL-uri separate oferă atât o bază bună pentru implementarea inițială, cât și suport pentru dezvoltări ulterioare.

Acest document SRS poate fi folosit atât pentru predare, cât și ca document de analiză și planificare pentru echipa de proiect.
