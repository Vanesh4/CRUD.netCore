
//console.log("te quiero <3")

var tabla = document.getElementById('miTabla');

//OCULTAR Y MOSTRAR COLUMNAS
function toggleColumns(columnIndexes) {
    var filas = tabla.rows;

    // Toggle the class 'hidden' for the specified columns in all rows
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
var zoomLevel = 0.8;
tabla.style.zoom = zoomLevel;

function zoomIn() {
    zoomLevel += 0.1;
    applyZoom();
}

function zoomOut() {
    zoomLevel -= 0.1;
    applyZoom();
}

function applyZoom() {
    /*var tabla = document.getElementById('miTabla');*/
    tabla.style.zoom = zoomLevel;
}


document.addEventListener('DOMContentLoaded', function () {
    tabla.style.display = "block";
    

    //SELECCIONAR LAS CELDAS
    /*var tabla = document.getElementById('miTabla');*/
    var celdas = tabla.querySelectorAll('td');
    var celdaSeleccionada = null;

    tabla.addEventListener('click', function (event) {
        var celda = event.target;
        marcarCeldaSeleccionada(celda);
    });

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