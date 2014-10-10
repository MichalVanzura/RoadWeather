RoadWeather
===========

##Oficiální zadání
- Mobilní responzivní web s mapou zobrazující informace z nějaké služby. Komunikace bude probíhat z browseru na serverové API, které dále udělá request na službu třetí strany a vrátí výsledky.
- Technologie: MVC, WebApi.
-	Napište mobilní webovou aplikaci, která zobrazí mapu, ve které půjde vyhledávat trasu z místa A do B.
-	Do aplikace se kromě zadání místa A a B vloží taky čas, kdy bude cesta začínat.
-	Po zapnutí webu se rovnou objeví mapa zobrazující aktuální polohu.
-	Aplikace pak na základě vstupních informací zobrazí předpověď počasí v průběhu celé trasy, s ohledem na místo a čas, kdy bude uživatel v daném místě projíždět. Předpověď počasí bude zobrazena přímo v mapě (ikonka počasí a teplota).
-	Celá aplikace bude přizpůsobena pro mobilní zařízení.

##Finální podoba zadání:
-	Uživatel zadá začátek a konec trasy na mapě, případně další waypointy (maximálně 8) na trase a datum/čas začátku cesty.
  - Čas bude zohledněn, pokud konec cesty bude do 5 dní 
  - Pro vzdálenější plánování dokážeme poskytnout předpověď až na 16 dní s jedním údajem pro počasí (zataženo/ jasno…) na den a pro rozsah teplot během tohoto dne
-	Podporována bude cesta automobilem, na kole a pěšky 
-	Cesta se vypočte, trasa se zobrazí se na mapě s ikonami počasí podél trasy
-	Uživatel si může také zobrazit textový seznam instrukcí cesty 
-	a také seznam údajů o počasí, jaké bude během cesty v ‘určitých’ intervalech
  - Velikosti těchto intervalů budou vhodně dopočítány v závislosti na zvoleném způsobu dopravy
  
###Služby, které chceme použít: 
- http://openweathermap.com
- https://developers.google.com/maps/documentation/directions/

###Speciální případy
-	V případě chůze
  - Krátkodobá předpověď do 5 dní: 
    - Kratší než 3 hodiny bude aplikace vracet pouze jeden údaj o počasí.
    - Pokud bude chůze delší, vrátíme více údajů. 
  - Dlouhodobější předpověď na dobu vzdálenější než 5 dní:
    - Vrátíme pouze jeden údaj (případně rozsah teplot během dne) 
    - Až když bude trasa delší než 3 hodiny a délka trasy více než 50 km, budeme předpokládat, že by se počasí mohlo výrazně změnit z  
    - Jinak budeme předpokládat, že člověk neurazí dostatečnou vzdálenost pěšky, aby se počasí nějak výrazně změnilo.
-	Pro jízdu autem / na kole
  -Budeme vracet více údajů i pro plánování vzdálenější než 5 dní. Můžeme totiž urazit vzdálenost delší, aby se projevilo, že jinde je (výrazně) jiné počasí.
