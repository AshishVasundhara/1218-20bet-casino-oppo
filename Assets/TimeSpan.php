<?php
date_default_timezone_set("America/New_York");
$dt = new DateTime();
echo $dt->format('m-d-Y/H:i:s');
?>