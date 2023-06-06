# PEC2 First Person Shooter Game

## Index

- [Cómo jugar](https://gitlab.com/Tomas-Gayo/pec2-firstpersonshootergame#c%C3%B3mo-jugar)
- [Demo](https://gitlab.com/Tomas-Gayo/pec2-firstpersonshootergame#demo)
- [Puntos importantes](https://gitlab.com/Tomas-Gayo/pec2-firstpersonshootergame#puntos-importantes)
- [Librerías](https://gitlab.com/Tomas-Gayo/pec2-firstpersonshootergame#librer%C3%ADas)
- [Atribuciones](https://gitlab.com/Tomas-Gayo/pec2-firstpersonshootergame#atribuciones)

## Cómo jugar

Este shooter se controla con teclado y ratón. Para el movimiento del personaje podemos utilizar las teclas de dirección **W (adelante), S (atrás), D (giro derecha) y A (giro izquierda)**, también, se puede saltar con el espacio o correr con la tecla "Shift". Para cambiar de arma podemos presionar las teclas 1 y 2 del teclado. Por otro lado, para poder disparar utilizaremos el raton con el click izquierda.

## Demo

Se puede ver la [demostración del juego en video](https://www.youtube.com/watch?v=aRwNpOQlPVc) en Youtube para su versión de PC. 
También se puede [descargar el ejecutable en itch.io](https://toyoerin.itch.io/uoc-pec2-shooter-game).

## Puntos importantes

- **La parte inicial del escenario deberá estar ubicada en un terreno montañoso, pudiendo también hacer partes urbanas, y luego pasará a ser un entorno cerrado dentro de algún tipo de edificio o instalación.**

El primer escenario ocurre en un terreno montañoso donde el objetivo será atravesarlo para encontrar una base militar. Una vez llegamos a este punto se nos mostrará un nuevo escenario que será el que corresponde a la parte interior. Esta parte interior contiene pequeñas zonas exteriores una habitación con un jefe final.

<img src="/images/Forest.PNG" alt="Escena bosque" width="500"/>

<img src="/images/militarybase.PNG" alt="Escena base militar" width="500"/>

<img src="/images/bossroom.PNG" alt="Habitación de jefe final" width="500"/>

- **El personaje deberá tener dos tipos de arma. Una más pequeña, de cadencia lenta y precisa como podría ser una pistola, y otra más de largo alcance y de fuego rápido como podría ser una ametralladora.**

El arma principal es un arma de cadencia pequeña, con gran potencia, rango de disparo corto y pocas balas, se podría relacionar con una escopeta. En cambio, el arma secundaria es un arma de cadencia alta (tipo automática), con poca potencia y mucho rango pero con gran cantidad de balas. 

Por la parte técnica, se ha utilizado un scriptable object para contener sus datos. Pero la lógica se efectua en el script PlayerShooter.cs.

<img src="/images/arma1.PNG" alt="Arma principal" width="500"/>

<img src="/images/arma2.PNG" alt="Arma secundaria" width="500"/>

- **A parte de su barra de vida, el personaje tendrá también una barra de escudo. Cuando le disparen, si tiene aún escudo, será el escudo quien reciba la mayor parte del daño, pero aun así deberá perder un poco de vida. Cuando el escudo llegue a 0, la vida se llevará el 100% del daño.**

Cuando los enemigos golpeen al jugador se llamara a la funcion Damage() en el script PlayerHealth.cs. Esta controla exactamente lo que se pide en el enucniado. 
`public void Damage(int damage)
    {
        // Substract shield the maximum of damage
        currentShield -= damage;

        if (currentShield <= 0)
        {
            // In case the shield is removed ...
            currentShield = 0;
            // ... subsract the maximum of damage to the health
            currentHealth -= damage;
        }
        // If there is still shield substract a part of health only
        else
            currentHealth -= damage/10;

        // If there's no life you lose
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameManager.instance.UpdateGameState(GameState.Lose);
        }
    }`


- **Tanto la cantidad de vida, cómo de escudo, el tipo de arma y la munición deberán siempre verse por pantalla (HUD).**

En la siguiente imagen se pueden ver tanto la vida y escudo del jugador como la cantidad de munición y tipo de arma que tiene en ese mismo instante el jugador. 

<img src="/images/HUD.PNG" alt="HUD del personaje." width="500"/>

- **El escenario deberá tener plataformas móviles, ya sean ascensores o desplazamientos horizontales.**

Hay dos elementos móviles en los escenarios. El primero de todos es la puerta, que se abre o se cierra dependiendo de si detecta al jugador o no. De la misma manera, hay un ascensor que se moverá verticalmente. 

<img src="/images/elevator.PNG" alt="ascensor arriba." width="500"/>

<img src="/images/elevator2.PNG" alt="ascensor abajo." width="500"/>


- **Deberán haber puertas que no puedan abrirse si el player no tiene la llave adecuada. Así se irán generando puzles.**

Se han repartido llaves por el escenario que abren puertas especificas. Si el jugador no consigue la llave, la puerta no se abrirá al acercarse. Por otro lado, se han colocado en puntos estrategicos para que el jugador tenga que pasar por todas las zonas. 

<img src="/images/key.PNG" alt="Llave del juego" width="500"/>

- **Deberá haber "enemigos" que patrullen por la escena y que si te ven se pongan a dispararte. Si pasas cerca suyo pero no te han visto (simulando que han oído tus pasos) deberán dar una vuelta de 360 grados buscándote antes de seguir con su patrulla si no te ven.**

Tenemos dos tipos de enemigo el drone y la araña. El dron es un enemigo a distancia, patrullará su zona hasta que encuentre al jugador, eso lo pondrá en modo alerta y lo perseguirá cuando lo descubra, si esta en rango comenzará disparar ráfagas de tres balas. La araña es un enemigo que al detectar al jugador empezará a perseguirlo hasta que explota haciendo mucho daño de area.

Además, han añadido dos jefes que son copias de los enemigos normales pero son más grandes y con más vida. Estos tendrán un papel más importante porque dejarán caer objetos para avanzar en el juego.

<img src="/images/drone.PNG" alt="Enemigo dron" width="500"/>

<img src="/images/spider.PNG" alt="Enemigo araña" width="500"/>

- **Esparcidos por el escenario deberán haber ítems de 'vida', 'escudo' y 'munición'. Los enemigos al morir deberían dejar alguno en el suelo. También podrían dejar llaves.**

Hay objetos esparcidos por el escenario que curan, recuperan escudo y recargan munición, además de poder ser instanciadas por los enemigos al morir. Este punto es una de las mecánicas más importantes de este juego puesto que algunos enemigos dejan caer llaves al morir.

<img src="/images/medicine.PNG" alt="Medicina" width="500"/>

<img src="/images/shield.PNG" alt="Pretección" width="500"/>

<img src="/images/ammo.PNG" alt="Munición" width="500"/>

Un extra en este punto es una alarma que al destruirla deja caer una llave y permite progresar en el nivel.

<img src="/images/alarm.PNG" alt="Alarma" width="500"/>

- **El juego deberá tener pantalla de game over y que al morir podamos reiniciar el nivel.**

El game manager gestiona el flujo del juego a través de estados. Según los diferentes eventos del juego, se irá avanzando en la partida. Uno de estos estados es el de game over. Si acabamos el juego por muerte podremos reinciar el nivel por donde lo dejamos (por ejemplo, si el jugador esta en la base militar y muere, no tendrá que hace de nuevo la escena de bosque). Si acabamos el juego por victoria podremos reniciar el juego también. 

<img src="/images/gameover.PNG" alt="Game Over pantalla" width="500"/>

<img src="/images/menu.PNG" alt="Menu nueva partida" width="500"/>

<img src="/images/menu2.PNG" alt="Menu después de morir" width="500"/>

## Librerías

Se ha utilizado gran cantidad de assets de la store. En orden de importancia son:

- **Sci-Fi Arsenal**: casi todos los sistemas de particulas y sonidos se han sacado de este asset, por ejemplo, explosiones, fuentes de particulas y la bala del enemigo.  

- **Sci-Fi UI**: la UI se ha obtenido de este asset. Incluye botones, backgrounds y algunos sonidos. 

- **Sci-Fi Lite**: la base militar se ha hecho con los prefabs de esta librería: pasillos, puertas, ascensores...

- **Dreamscape Nature**: Meadows**: para el terrain hemos utilizado texturas y los arboles de este paquete. 

- **PopupAsylum**: para hacer los enemigos hemos utilizado los modelos de este asset. 

- **Modern Weapon Pack**: Ambas armas provienen de aquí. 

- **SkySeries Freebie**: como bien dice su nombre, el cielo se ha escogido dentro de la selección de este paquete.

- **boxes_pack**: boxes pack contiene una serie de cajas con diseños convenientes que se pueden utilizar para decorar, además, aquí se ha hecho uso para los objetos de cura, recuperar escudo y munición.

- **Standard Assets**: la hierva del terrain se ha conseguido en este paquete.  

- **Rust Key**: modelo de la llave. 

- **Text Mesh Pro**: este asset se ha utilizado para mejorar los estilos de los textos en la UI. Funciona igual que los textos normales pero tiene más opciones de personalización.

## Atribuciones

- **Sonido ambiente noche**: [Usuario hdfreema](https://freesound.org/people/hdfreema/sounds/333221/)

- **Música final (hover)**: [Usuario Mega Pixel Music Lab](https://opengameart.org/content/sad-piano-0)

- **Sonido game over**: [Usuario LorenzoTheGreat](https://freesound.org/people/LorenzoTheGreat/sounds/417795/)

- **Sonido victoria**: [Usuario joshuaempyre](https://freesound.org/people/joshuaempyre/sounds/404024/)

- **Sonido alarma**: [Usuario melokacool](https://freesound.org/people/melokacool/sounds/613650/)

- **Sonido puerta abriendose**: [Usuario MrAuralization](https://freesound.org/people/MrAuralization/sounds/158626/)

- **Sonido puerta bloqueada**: [Usuario theplax](https://freesound.org/people/theplax/sounds/618145/)

- **Sonido disparo AK47**: [Usuario LeMudCrab](https://freesound.org/people/LeMudCrab/sounds/163457/)

- **Decal**: [Usuario musdasch](https://opengameart.org/content/bullet-decal)
