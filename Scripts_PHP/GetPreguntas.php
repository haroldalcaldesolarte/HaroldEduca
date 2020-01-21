<?php

require_once 'config.php';
//Cogiendo categoria padre que envia la petición
$nombreCatPadre = $_POST["nombreCatPadre"];

// Create connection
$conn = new mysqli(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

//Obtengo id de la categoria que estoy recibiendo
$query_id = "SELECT id_cat FROM categoria WHERE nombre_cat = '" . utf8_decode($nombreCatPadre) . "'";
$result_id = $conn->query($query_id);
$result_array = array();

if ($result_id->num_rows > 0) {
    while($row = $result_id->fetch_assoc()) {
        $id = $row["id_cat"];
    }
} else {
    $id = NULL;
}

if (!is_null($id)) {
	$count_preguntas = "SELECT count(*) as total FROM pregunta WHERE id_categoria = '" . $id . "';";
	$resultado = $conn->query($count_preguntas);
	if ($resultado->num_rows > 0) {
		while ($row = $resultado->fetch_assoc()) {
			$total = $row['total'];
		}
	}
	else{
		$total = 0;
	}

	if ($total >= 4){ // PONER EL LIMITE QUE LE PASE
		$preguntas_sql = "SELECT id_pregunta,descripcion,dificultad,id_categoria FROM pregunta WHERE (SELECT count(*) 
						FROM respuesta WHERE id_pregunta = id_preg) = 4 AND id_categoria = '" . $id ."'ORDER BY RAND() LIMIT 4" ;
		$preguntas = $conn->query($preguntas_sql);
		if ($preguntas->num_rows > 0) {
			while ($row = $preguntas->fetch_assoc()) {
				array_push($result_array, Array('id_pregunta' => $row['id_pregunta'], 'descripcion' => utf8_encode($row['descripcion']), 'dificultad' => $row['dificultad'], 'id_categoria' => $row['id_categoria']));
			}
			echo json_encode($result_array);
		}else{
			echo "0";
		}
	}else{
		echo "0";
	}
}else{
	echo "No existe la categoria";
}

$conn->close();

?>