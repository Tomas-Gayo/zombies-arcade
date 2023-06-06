# PEC3 PlatformsGame

Nota: He tenido muchos problemas los últimos días con el juego por rendimiento y también al crear la build. Con lo primero no ha habido problema, sin embargo, al crear la build pasan errores que no ocurren en unity. Parece que en el game manager no soy capaz de encontrar al jugador y, por lo tanto, no se puede morir. Si se hace la build y el personaje muere, de momento se puede pausar el juego y finalizarlo desde ese menu. 
 
## Index

- [Cómo jugar](https://gitlab.com/Tomas-Gayo/pec3-platformsgame#c%C3%B3mo-jugar)
- [Demo](https://gitlab.com/Tomas-Gayo/pec3-platformsgame#demo)
- [Puntos importantes](https://gitlab.com/Tomas-Gayo/pec3-platformsgame#puntos-importantes)
- [Librerías](https://gitlab.com/Tomas-Gayo/pec3-platformsgame#librer%C3%ADas)
- [Atribuciones](https://gitlab.com/Tomas-Gayo/pec3-platformsgame#atribuciones)

## Cómo jugar

Este juego se controla con teclado y ratón. Para el movimiento del personaje podemos utilizar las teclas de dirección **W (adelante), S (atrás), D (giro derecha) y A (giro izquierda)**, también, se puede saltar con el espacio o correr con la tecla "Shift". También se puede disparar con click izquierdo y apuntar con el click del ratón derecho.

## Demo

Se puede ver la [demostración del juego en video](https://youtu.be/m2oGytLDo28) en Youtube para su versión de PC. 

## Puntos importantes

- **Crear un pequeño pueblecito con los assets disponibles en el ejemplo de los materiales u otros si queréis.**

El escenario esta ambientado en un puerto comercial. 

<img src="/images/game scene 1.PNG" alt="Puerto vista 1" width="500"/>

<img src="/images/game scene 2.PNG" alt="Puerto vista 2" width="500"/>


- **El personaje deberá tener una pistola para disparar en línea recta hacia adeltante siempre (no hace falta apuntar).**

El jugador puede disparar hacia delante además de apuntar haciendo zoom hacia delante. 

- **El personaje deberá estar completamente animado. Idle, caminar, correr, saltar, girar, disparar, recibir impacto y morir.**

El personaje esta animado con las animaciones requeridas. Se ha creado un animator desde cero y de ha añadido en un blend tree las animaciones de caminar y correr. Para ompimizar tiempo, ambas son la misma animacion pero la segunda se ha subido la velocidad de animación. Las demás animaciones no son tan complejas y se ha obtado por crear un estado simple, no obstante, se ha hecho uso del Animation Rigging para hacer la animación de disparo. Por lo que esa animación no forma parte de la máquina de estados. 

<img src="/images/player animator.PNG" alt="Animator del personaje" width="500"/>

- **Tanto la cantidad de vida como la munición deberán siempre verse por pantalla (HUD).**

En la siguiente imagen se pueden ver tanto la vida y como la cantidad de munición. La munición en esta ocasion es por porcentaje. 

<img src="/images/HUD.PNG" alt="HUD del personaje." width="500"/>

- **Los enemigos serán zombies que irán paseando por la ciudad y cuando el player esté cerca irán siempre caminando hacia el personaje y lo atacarán cuando esté a su lado.**

Los enemigos estan repartidos por el escenario deambulando y buscando al jugador, cuando esta a suficiente distancia podrán atacar. Hay dos tipos de enemigos, uno que esta tirado en el suelo y debe levantarse al detectar al jugador a una cierta distancia y otro que siempre esta de pie, sin embargo, su funcionamiento es muy parecido. 

<img src="/images/zombie.PNG" alt="Enemigo 1" width="500"/>

<img src="/images/zombie 2.PNG" alt="Enemigo 2" width="500"/>


- **Los enemigos deberán tener animación de caminar, atacar, morir y recibir impacto.**

Los enemigos tienen todas las animaciones requeridas, incluso, como se ha mencionado en el partado anterior, se ha añadido un zombie que se levanta del suelo. 

<img src="/images/enemy animator.PNG" alt="Animator del enemigo" width="500"/>

- **Poner sistemas de partículas cuando te hieren o cuando un zombie es golpeado o muere.**

Hay sistemas de particulas varias. Por ejemplo, cuando un enemigo recibe un impacto o muere, cuando el jugador dispara o cuando la bala golpea un objeto. 

- **Repartidos por el escenario deberán haber ítems de ‘vida’ y ‘munición’.**

Hay objetos esparcidos por el escenario que curan y recargan munición, además de poder ser instanciadas por los enemigos al morir. 

<img src="/images/heal.PNG" alt="Medicina" width="500"/>

<img src="/images/ammo.PNG" alt="Munición" width="500"/>

Un extra en este punto es un objeto que al recogerlo da puntuos. De esto hablaremos más adelante. 

<img src="/images/extra.PNG" alt="Objeto extra" width="500"/>

- **El juego deberá tener pantalla de game over y que al morir podamos reiniciar el nivel.**

El game manager gestiona el flujo del juego a través de un game loop. Aprovechando el sistema de día y noche que se pedía en la teoría, se ha contruido un sistema basado en la supervivencia por rondas en el que cada ronda es 1 día, por lo tanto, hay ciclo de día y noche. En cada período el jugador deberá realizar diferentes objetivos. Además por el día habrá zombies más débiles y por la noche habrán zombies más rápidos y fuertes.

<img src="/images/game over.PNG" alt="Game Over pantalla" width="500"/>

<img src="/images/menu.PNG" alt="Menu nueva partida" width="500"/>

También hay un menu de pausa para que el jugador pueda salir fácilmente del loop. 
<img src="/images/pause menu.PNG" alt="Menu Pausa" width="500"/>

- **Extras.**
Además de lo ya comentado, para que el juego tenga un objetivo se ha añadido un sistema de puntuaciones que dependeran del ciclo de día y noche. Por lo que un incentivo para el jugador es buscar unos objetos lilas repartidos por el escenario en lugares donde hay que buscar la manera de llegar, ya que no estarán dispuestos para un acceso rápido. 

## Librerías

Se ha utilizado gran cantidad de assets de la store. En orden de importancia son:

- **ToonHarborPack**: la mayoría de modelos y personajes son de este asset. 

- **Sci-Fi UI**: la UI se ha obtenido de este asset. Incluye botones, backgrounds y algunos sonidos. 

- **Sci-Fi Arsenal**: los items se han sacado de este asset.

- **EffectExamples**: La mayoría de efectos de particulas como la sangre o el decal son de aquí. 

- **BigRookGames**: Este es el pack con el arma del personaje. 

- **Standard Assets**: las animaciones del personaje son de este asset.  

- **Zombie_27**: contiene las animaciones del zombie.

- **Text Mesh Pro**: este asset se ha utilizado para mejorar los estilos de los textos en la UI. Funciona igual que los textos normales pero tiene más opciones de personalización.

## Atribuciones

- **Música noche**: [Usuario maxstack](https://opengameart.org/content/hold-the-fort)

- **Música día**: [Usuario playgb.com](https://opengameart.org/content/adventures-of-yuki-level-1-music)

- **Música Menu**: [Usuario LushoGames](https://opengameart.org/content/simple-action-beat)

- **Sonido objeto lila**: [Usuario NenadSimic](https://freesound.org/people/NenadSimic/sounds/171696/)

- **Sonido curación**: [Usuario Zoltan Mihalyi](https://opengameart.org/content/heal)

- **Sonido zombie**: [Usuario TRNGLE](https://freesound.org/people/TRNGLE/sounds/390614/)


