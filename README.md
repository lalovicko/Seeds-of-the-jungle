# 🌿 Seeds of the Jungle

**Desarrollador:** Jesús Eduardo Cruz Hernandez  
**Materia:** Tópicos Avanzados de Programación  
**Profesor:** Francisco Emiliano Aguayo Serrano  
**Cuatrimestre:** 5°A - Universidad Cuauhtémoc  

## 📝 Descripción del Proyecto
*Seeds of the Jungle* es un juego de acción y supervivencia en perspectiva Top-Down. El jugador controla a una planta carnívora ancestral en lo profundo de una selva mística. La misión es recolectar las semillas vitales esparcidas por el terreno para restaurar el equilibrio, mientras se defiende de los murciélagos sombra que emergen de nidos oscuros para detenerla.

---

## 🚀 Mecánicas Principales

### 1. Sistema de Movimiento y Recolección
* **Movimiento Orgánico:** Control fluido de la planta mediante `Rigidbody2D` y el nuevo `Input System` de Unity.
* **Coleccionables (Seeds):** El núcleo del juego consiste en recolectar semillas que actualizan el HUD dinámicamente y activan las condiciones de victoria.

### 2. Habilidades Especiales 
Se implementaron tres mecánicas que añaden profundidad táctica y equilibran la dificultad:

* **🌀 Percepción Lenta (Slow Motion):** Mediante la tecla `Shift`, la planta entra en un estado de concentración que ralentiza el tiempo al 50%. Gracias a la compensación física, el jugador mantiene su velocidad normal, obteniendo una ventaja crítica para esquivar ataques.
* **🛡️ Escudo de Espinas (Defensa Activa):** Al presionar `E`, la planta despliega una barrera protectora. Mientras el escudo está activo, la planta es invulnerable y puede eliminar enemigos o destruir nidos por contacto directo.
* **🕳️ Nidos de Sombras (Generadores):** Obstáculos del entorno que generan murciélagos de forma periódica. Estos nidos son el principal desafío y solo pueden ser erradicados usando el Escudo de Espinas.

---

## 🛠️ Especificaciones Técnicas

* **Motor:** Unity 2022.3 LTS.
* **Lenguaje:** C# con enfoque en programación orientada a objetos.
* **Arquitectura:** * **GameManager:** Control central de la lógica de victoria y conteo de semillas.
    * **UIStatemanager:** Manejo de estados de interfaz (Play, Win, Lose).
    * **Corrutinas:** Gestión de estados temporales para el Slow Motion y Cooldowns de habilidades.
    * **Física:** Uso de capas de colisión (Layers) para diferenciar entre interacciones físicas y disparadores (Triggers).

---

## 🎮 Controles

| Acción | Tecla |
| :--- | :--- |
| **Moverse** | `W`, `A`, `S`, `D` |
| **Percepción Lenta** | `Left Shift` |
| **Escudo de Espinas** | `E` |

---
