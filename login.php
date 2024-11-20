<?php
$servername = "localhost:3306";
$username = "root";
$password = "";
$dbname = "jgb_db";

$loginEmail = $_POST["loginEmail"];
$loginPass = $_POST["loginPass"];

$conn = new mysqli($servername,
				   $username,
				   $password,
				   $dbname);

$sql =
"SELECT * FROM jgb_login WHERE mail = '" . $loginEmail . "'";
$result = $conn->query($sql);

if($result->num_rows > 0) {
	while($row = $result->fetch_assoc()) {
		if($row["password"] == $loginPass) {
			echo "Login success!!";
			exit;
		}
	}
	echo "Wrong password..";
} else {
	echo "ID not found..";
}

$conn->close();
?>