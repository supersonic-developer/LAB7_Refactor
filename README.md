# Turkmite laboratórium

Ennek a laboratóriumnak a célja az alapvető C# fejlesztési eszközök (Visual Studio szerkesztő funkciói, debug alapok, nuget packagek és unit tesztek) gyakorlása. A kiinduló állapot egy már működő program, ami messze van a szépen megtervezett, objektum orientált programtól, így az egyes feladatok során a cél az objektumokra bontás, a helyes működés ellenőrzése unit tesztekkel, valamint utána a továbbfejlesztés.

Rendhagyó módon a feladat és majdnem a teljes megoldás is megtalálható egy snippet formájában itt:
http://bmeaut.github.io/snippets/snippets/0220_TurkmiteRefactorEsTeszt/

Ezzel az a célunk, hogy ne lehessen véglegesen megakadni a feladat megoldásával. Viszont hangsúlyozottan szeretnénk mindenkit megkérni, hogy a feladatokat ne copy-paste jelleggel oldja meg, hanem törekedjen a feladat megértésére, majd utána próbálja meg önállóan megoldani. Nyilván kimásolni könnyebb, de ez a későbbi laborfeladatoknál fog visszaütni, amikor az itt gyakorolt eszközök ismeretét már feltételezzük.

## Felkészülés a laborra

- Felkészülésként előre olvasd el az egész fenti snippetet! Kövesd végig a módosításokat, figyeld meg, mi miért lett olyan, amilyen.
- Nézd meg a laborra felkészülést támogató videókat:
  - (Lásd Moodle)
  - Gondosan olvasd végig a [snippetet](http://bmeaut.github.io/snippets/snippets/0220_TurkmiteRefactorEsTeszt/) is, mert szerepelnek benne olyan trükkök, ami a videóban nem kerül sorra.

- Próbáld ki Visual Studio alatt, hogyan lehet egy .NET Core xUnit teszt projektet létrehozni.

## A laboron

(A labor elején, már most hozz létre egy új branchet és utána azon dolgozz, hogy a pull requestet könnyű legyen majd a labor végén létrehozni! És ha az egyetemen kezdted el a munkát, mielőtt elmész, ne felejtsd el felpusholni a változásokat a laborgépről a githubra!)

- A laboron végezd el ugyanezeket a módosításokat, de úgy, hogy nem másolod a forráskódot, hanem a feladatra koncentrálva írod át. (Közben természetesen lesznek olyan részek is, ami a snippetben nincs részletekbe menően kifejtve, mint például a unit teszt létrehozás. Itt lehet, hogy szükség lesz egy kis kísérletezésre vagy Google keresésre. Nyilván nem te leszel az első, aki rákeres, így hamar meg lesz a válasz.)
- A megoldás során ne felejts el minden feladat után commitolni!
- A snippet anyaga után a maradék időben menj tovább az alábbi feladatokkal.
- A labor végén ne felejtsd el létrehozni a pull requestet és hozzárendelni a laborvezetődet, hogy meg tudja nézni.

## A snippet utáni labor feladatok

A további feladat egy 3 színű, és egy tetszőleges, valamivel összetettebb Turkmite elkészítése, unit teszt támogatással. Ha van kedved kipróbálni a test first megközelítést, elkészítheted először a unit teszteket is és utána magát a "production kódot".

- Készíts egy új Turkmite osztályt a következő mozgási szabályokkal:
    - A turkmite 3 színű: fekete, piros, sárga.
    - Amikor fekete mezőn áll, megnövel egy belső számlálót. Ha az új számláló érték páros, akkor pirosra színez és balra fordul, ha páratlan, akkor sárgára színez és szintén balra fordul.
    - Piros mezőn sárgára fest és balra fordul.
    - Sárga mezőn feketére fest és jobbra fordul.
- Készíts unit teszteket, melyek ellenőrzik a fenti szabályok helyes működését.

(Commitold be az eddigi eredményeket!)

Ha még maradt időd, kiegészítő feladatként készíts egy általad kitalált turkmite-et! Ha tetszik a generált kép, küldd be a tantárgyi Teamsbe és lájkold a többiek munkáját! :)

## A labor befejezése

Az elkészült megoldásról készíts pull requestet és küldd be, hozzárendelve a laborvezetődet reviewerként.
