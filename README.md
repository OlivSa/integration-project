# integration-project
Maventa Harvest Integration Project

In Finnish:

Tässä on pieni kuvaus ratkaisusta:
1. Tämä on MVC sovellus
2. Joka lauantai (käytin Quartz.net Scheduler) se tarkistaa viime viikon hyväksytyt tunnit Harvestista
3. Joka lauantai kaikki hyväksytyt tunnit ja tiedot niistä (projektien nimet, tiedot asiakkaista etc. ) generoidaan laskuiksi
4. Joka lauantai uudet laskut ilmestyvät MVC sivulle sekä lähetetään Maventaan
