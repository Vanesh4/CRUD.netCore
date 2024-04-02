document.addEventListener('DOMContentLoaded', function () {
    console.log("te amoooooooooooooooou <3")
    var tabla = document.getElementById('miTabla');
    toggleColumns([2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16]);
});
//OCULTAR Y MOSTRAR COLUMNAS
var tabla = document.getElementById('miTabla');
function toggleColumns(columnIndexes) {
    boton = document.getElementById('ocultarColumnas');
    if (boton.textContent === "Mostrar") {
        boton.textContent = "Ocultar";
    } else {
        boton.textContent = "Mostrar";
    }
    var filas = tabla.rows;
    // Toggle the class 'hidden' for the specified columns in all rows
    for (var i = 0; i < filas.length; i++) {
        //console.log("parametro " + columnIndexes)
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
    var tabla = document.getElementById('miTabla');
    tabla.style.zoom = zoomLevel;
}


document.addEventListener('DOMContentLoaded', function () {
    var tabla = document.getElementById('miTabla');
    tabla.style.display = "block";
    

    //SELECCIONAR LAS CELDAS
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