<?php
$servername = "localhost:3306";
$username = "root";
$password = "";
$dbname = "jgb_db";


$conn = new mysqli($servername,
				   $username,
				   $password,
				   $dbname);


  $loginUser = $_POST["loginUser"];
  $loginEmail = $_POST["loginEmail"];
  $loginPass = $_POST["loginPass"];
  $loginCheckPass = $_POST["loginCheckPass"];

  
$jgb_sql = "SELECT * FROM jgb_login WHERE userid = '".$loginUser."'";
$jgb_result = $conn->query($jgb_sql);

$jgb_sql1 = "SELECT * FROM jgb_login WHERE mail = '".$loginEmail."'";
$jgb_result1 = $conn->query($jgb_sql1);


if ($jgb_result->num_rows > 0) 
{
    while($row = $jgb_result->fetch_assoc())
    {
        if($row["userid"] == $loginUser)
        {
            echo "1"; // ����� �̸��� �̹� �����մϴ�.
            exit;
        }
    }
}
elseif ($jgb_result1->num_rows > 0) 
{
        while($row = $jgb_result1->fetch_assoc())
    {
        if($row["mail"] == $loginEmail)
        {
            echo "2"; //���� �̹� �����մϴ�.
            exit;
        }
    }
}
elseif ($loginPass != $loginCheckPass) 
{
    // ��й�ȣ�� ��ġ���� �ʽ��ϴ�.
    echo "3";
    exit;
} 
else 
{
    $insert_sql = "INSERT INTO jgb_login (userid, password, mail) VALUES ('".$loginUser."','".$loginPass."','".$loginEmail."')";
    $conn->query($insert_sql);
}


//connect ����
$conn->close();
?>
