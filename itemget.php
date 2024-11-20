<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "jgb_db";

$conn = new mysqli($servername,
				   $username,
				   $password,
				   $dbname);

if($conn->connect_error) {
	die("connection failed: ".$conn->connect_error);
}

$sql = "SELECT * FROM jgb_item";
$result = $conn->query($sql);

if($result->num_rows > 0) {
	echo "[";
	while($row = $result->fetch_assoc()) {
		echo "{'id': '".$row['id'].
			 "', 'itemname': '".$row['itemname']."', 'information': '".$row['information']."'},";
	}
	echo "]";
}

$conn->close();
?>