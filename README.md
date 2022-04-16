# Grupp5-Grupparbete
Gruppuppgift
Webbutveckling .NET
Mäklarsystem ”Hemnet"
•Uppgiften är att bygga ett system där mäklare kan lägga upp objekt (lägenheter / hus) till försäljning
•Objekt visas sedan upp på en offentlig objektssida
•Spekulanter kan där skicka in intresseanmälningar
Komponenter
•Systemet ska ha följande komponenter:
•Datalager (SQL / Entity framework)
•ASP.NET MVC frontend (externa kundsidor)
•React-frontend (mäklargränssnitt)
•ASP.NET, REST-api (som React-appen använder)
Datalager (SQL / Entity framework)
•Ni ska bygga en datamodell med Entity framework
•Datamodellen ska designas för att kunna lagra information som krävs, t.ex.:
•Objekt (lägenheter / hus)
•Användare (kopplade till Google-inloggning)
•Mäklare
•Kunder
•Intresseanmälningar
ASP.NET frontend
•Med hjälp av ASP.NET och Razor-pages bygger ni en sida som presenterar varje objekt (objektssidan)
•På objektssidan visas:
•Adress (och karta utifrån denna)
•Bilder
•Pris
•Beskrivning
•Bostadstyp
•Upplåtelseform
•Antal rum
•Boarea / biarea / tomtarea
•Byggår
•Visningsdatum
•Kunder kan här även göra intresseanmälningar
•Använd de kunskaper ni har om SEO för att göra objektssidan sökbar på Google
React frontend
•En Single Page Application byggd i React
•Här kan mäklare
•Logga in med sitt Google-konto
•Lägga till, ändra och ta bort objekt
•Ladda upp bilder till objekt
•Se intresseanmälningar
ASP.NET, REST-api
•REST-api som React-appen använder för att posta, hämta, uppdatera, ta bort data
•API:et ska implementera autentisering så att endast behöriga mäklare kan göra anrop
Produktionssättning
•I uppgiften ingår att produktionssätta samtliga komponenter i Azure
Att tänka på / Tips
•Ni ska använda GitHub
•Formulärfält ska ha validering så man fyller i rätt typ av data
•Om fält inte validerar ska relevanta felmeddelanden visas upp
•Alla detaljer följer inte av denna beskrivning, ta inspiration av t.ex:
•Hemnet
•Booli
•Mäklares hemsidor
Inlämning / Presentation
•Inlämning sker i genom att koden laddas upp på Google Classroom senast den 4/5 kl 23:59
•Länk till GitHub ska finnas med i inlämningen
•Slutresultatet presenteras för klass och lärare den 5/5
