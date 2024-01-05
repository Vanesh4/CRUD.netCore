alert("hola?")
//OCULTAR Y MOSTRAR COLUMNAS
function toggleColumns(columnIndexes) {
    var tabla = document.getElementById('miTabla');
    var filas = tabla.rows;

 
    for (var i = 0; i < filas.length; i++) {
        console.log("parametro " + columnIndexes)
        for (var j = 0; j < columnIndexes.length; j++) {
            //console.log("filas "+filas[i].cells[columnIndexes[j]])
            celda = filas[i].cells[columnIndexes[j]];
            if (celda) {
                celda.classList.toggle('hidden');
            }
        }
    }
}

//ZOOM a la tabla
var zoomLevel = 1;
function zoomIn() {
    zoomLevel += 0.1;
    applyZoom();
}

function zoomOut() {
    zoomLevel -= 0.1;
    applyZoom();
}

function applyZoom() {
    var tabla = document.getElementById('miTabla');
    tabla.style.zoom = zoomLevel;
}

document.addEventListener('DOMContentLoaded', function () {
    //SELECCIONAR LAS CELDAS
    var tabla = document.getElementById('miTabla');
    var celdas = tabla.querySelectorAll('td');
    var celdaSeleccionada = null;

    // Agregar un evento clic a las celdas
    tabla.addEventListener('click', function (event) {
        var celda = event.target;
        marcarCeldaSeleccionada(celda);
    });

    // Agregar un evento de teclado para las flechas
    document.addEventListener('keydown', function (event) {
        if (celdaSeleccionada) {
            
            celdaSeleccionada.classList.remove('selected');

            // Obtener la posición actual de la celda seleccionada
            var indice = Array.from(celdas).indexOf(celdaSeleccionada);
            var celdasVisibles = Array.from(tabla.querySelectorAll('td:not([style*="display: none"])'));

            
            switch (event.key) {
                case 'ArrowLeft':
                    indice -= 1;
                    break;
                case 'ArrowRight':
                    indice = celdasVisibles.indexOf(celdaSeleccionada) + 1;
                    //indice += 1;
                    break;
            }

            
            //indice = Math.max(0, Math.min(celdas.length - 1, indice));
            indice = Math.max(0, Math.min(celdasVisibles.length - 1, indice));
            // Marcar la nueva celda seleccionada
            celdaSeleccionada = celdas[indice];
            marcarCeldaSeleccionada(celdaSeleccionada);
        }
    });

    function marcarCeldaSeleccionada(celda) {
        if (celdaSeleccionada) {
            celdaSeleccionada.classList.remove('selected');
        }
        celda.classList.add('selected');
        celdaSeleccionada = celda;
    }
});