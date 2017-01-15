<?php
error_reporting(0);
require_once("Rest.inc.php");
	class API extends REST
	{
	
	   public $data = "";

		// const DB_SERVER = "localhost";
		// const DB_USER = "editsoft_dev";
		// const DB_PASSWORD = "Team@123";
		// const DB = "editsoft_barometer";

		 const DB_SERVER = "localhost";
		 const DB_USER = "root";
		 const DB_PASSWORD = "";
		 const DB = "dissertation";		
		

/*
		const DB_SERVER = "gubappwebservices.esy.es";
		 const DB_USER = "u465786269";
		 const DB_PASSWORD = "123456";
		 const DB = "u465786269_gbu";
	*/	
		private $db = NULL;

	
    public function __construct(){
            parent::__construct();              // Init parent contructor
            $this->dbConnect();                 // Initiate Database connection
        }   
        /*
         *  Database connection 
        */
        private function dbConnect(){
            $this->db = mysql_connect(self::DB_SERVER,self::DB_USER,self::DB_PASSWORD);
            if($this->db)
                mysql_select_db(self::DB,$this->db);
        }   
        /*
         * Public method for access api.
         * This method dynmically call the method based on the query string
         *
         */
        public function processApi(){
            $func = strtolower(trim(str_replace("/","",$_REQUEST['rquest'])));
            if((int)method_exists($this,$func) > 0)
                $this->$func();
            else
                $this->response('',404);                // If the method not exist with in this class, response would be "Page not found".
        }
		private function index()
		{
			$msg=array("API Vesion "=>1.3,"API Description "=>"The API use by the Desetation System");
			$this->response($this->json($msg),200);
		}
	
		private function Student_detail_retrive()
		{
			
			$roll_no=$_POST['roll_no'];
			$password=$_POST['password'];
			$quer="select * from Student_Registration where `Roll_no`='$roll_no' and `password`='$password'";
			$squer=mysql_query($quer);
				
			if(mysql_num_rows($squer)>0)
			{
				$row=array();
				while($data=mysql_fetch_assoc($squer))
				{
					array_push($row,$data);
				}
				
				$this->response($this->json($row),200);
				//echo $this->json($row);
			}
			else
			{
				$message=array("response code"=>101,"response desc"=>"Failed","message"=>"Invalid User detail");
				$this->response($this->json($message));
			}
		}
		private function student_data_retr_dir_faculty()
		{
			$fac_id=$_POST['fac_id'];
			$quer="select * from student_registration where `Guide`='$fac_id' ";
			$sqqq=mysql_query($quer);
			$row=array();
			while($data=mysql_fetch_assoc($sqqq))
			{
				array_push($row,$data);
			}
			$this->response($this->json($row),200);
		}
		
	
		private function Student_detail_retrive_faculty()
		{
			
			$roll_no=$_POST['roll_no'];
			$fac_i=$_POST['facult_id'];
	//		$password=$_POST['password'];
			if(!empty($roll_no)) //&& !empty($password))
			{
				$quer="select * from Student_Registration where `Roll_no`='$roll_no' and 	`Guide`!='$fac_i' ";
				$squer=mysql_query($quer);
				if(mysql_num_rows($squer) >0)
				{
					$row=array();
				while($data=mysql_fetch_assoc($squer))
				{
					array_push($row,$data);
				}
				
				$this->response($this->json($row),200);
				}
				else
				{
					$message=array("responce_code"=>103,"response desc"=>"Your Student","message"=>"Its Under Guidence");
					$this->response($this->json($message));
				}
				//echo $this->json($row);
			}
			else
			{
				$message=array("response code"=>101,"response desc"=>"Failed","message"=>"Invalid User detail");
				$this->response($this->json($message));
			}
			
		}
		private function Student_Registary()
		{
			$name=$_POST["name"];
			$roll=$_POST["roll_no"];
			$branch=$_POST["branch"];
			$broad_area=$_POST["broad_area"];
			$topic=$_POST["topic"];
			$guide_name=$_POST["guide"];
			$any_other_info=$_POST["any_other_info"];
			$password=$_POST['password'];
			
			$quer="insert into student_registration(`Name`,`Roll_No`,`Branch`,`broad_area`,`topic`,`any_other_info`,`password`,`Guide`) values('$name','$roll','$branch','$broad_area','$topic','$any_other_info','$password','$guide_name')";
			if(mysql_query($quer))
			{
			$msg=array('response_code'=>100,'response_desc'=>"Success",'message'=>"Registraion Successfull");
			$this->response($this->json($msg),200);
			}
			else
			{
			$msg=array('response_code'=>102,'response_desc'=>"Sorry",'message'=>"Already Exist");
			$this->response($this->json($msg),200);
				
			}
			
		
		}
		private function faculty_Registraion()
		{
			$facul_key_id=$_POST['facaultu_key_id'];
			$faculty_code=$_POST['faculty_code'];
			$faculty_name=$_POST['faculty_name'];
			$password=$_POST['passwo'];

				
					
			$quer="insert into faculty_registraion(`Faculty_Code`,`Faculty_Name`,`Password`) values('$faculty_code','$faculty_name','$password')";
			if(mysql_query($quer))
			{
			$msg=array('response_code'=>100,'response_desc'=>"Success",'message'=>"Registraion Successfull");
			$this->response($this->json($msg),200);
				}
				else
				{
					$msg=array('response_code'=>102,'response_desc'=>"Sorry",'message'=>"Already Exist");
					$this->response($this->json($msg),200);
			
				}
			
			
		}
		private function faculty_login()
		{
			$faculty_id=$_POST['faculty_id'];
			$faculty_password=$_POST['faculty_password'];
			$query="select * from 	faculty_registraion where Faculty_Code='$faculty_id' and Password='$faculty_password'";
			$pros_query=mysql_query($query);
		//	$sql=mysql_fetch_array($pros_query);
		if(empty($faculty_id) && empty($faculty_password))
		{
			$msg=array('response_code'=>301,'response_desc'=>"Not Filled","message"=>"???");
				$this->response($this->json($msg),225);
		}
		else
			if(mysql_num_rows($pros_query) >0)
			{
				$msg=array('response_code'=>100,'response_desc'=>"Login Success","message"=>"Valid User");
				$this->response($this->json($msg),200);
			}
			else
			{
				$msg=array('response_code'=>101,'response_desc'=>'Invalid Deatail',"message"=>"Invalid User");
				$this->response($this->json($msg));
			}
			
		}
		private function marks_evaluation()
		{
			$facult_id=$_POST['facult_id'];
			$student_id=$_POST['studen_id'];
			$report_marks=$_POST['report_marks'];
			$tech_marks=$_POST['technical_marks'];
			$presentation_marks=$_POST['presentation_marks'];
			$misc_mass=$_POST["misc_marks"];
			$typ=$_POST["type"];
			$total=$_POST['total'];
			
			$remark=$_POST['remark'];
			if(!empty($facult_id) && !empty($student_id))// && !empty($tech_marks) && !empty($presentation_marks) && !empty($tech_marks))
			{
				$quer1="select `ID` from `evaluation` where `Faculty_Code`='$facult_id' and `Student_id`='$student_id'";
				$rest=mysql_query($quer1);
				$s_id=array();
				$kk=-1;
				$sam_id=mysql_fetch_assoc($rest);//)
				$kk=$sam_id["ID"];
				$n_ro=mysql_num_rows($rest);
			
				if($n_ro==0)
				{
				
				$query2="insert into evaluation(`Faculty_Code`,`Student_id`,`Report_Writting`,`Technical_Content`,`Presentaion`,`planning_impl_rest`,`Total`,`remark`,`type`) values('$facult_id','$student_id','$report_marks','$tech_marks','$presentation_marks','$misc_mass',$total,'$remark','$typ')";
				mysql_query($query2);
				
				$msg=array('response_code'=>100,'response_desc'=>"Success",'message'=>"Registraion Successfull");
				$this->response($this->json($msg),200);	
				}
				else
				{
				$query2="UPDATE `evaluation` SET `Report_Writting` = '$report_marks', `Technical_Content` = '$tech_marks', `Presentaion` = '$presentation_marks',`planning_impl_rest`='$misc_mass' ,`Total` = '$total' ,`remark`='$remark',`type`='$typ' WHERE `evaluation`.`ID` = $kk";
				mysql_query($query2);
				
				$msg=array('response_code'=>110,'response_desc'=>"Success",'message'=>"Updated ");
				$this->response($this->json($msg),200);	

					
				}
				
			}
			
			else
			{
				$msg=array('response_code'=>101,'response_desc'=>"Failed",'message'=>"Invalid Detail");
				$this->response($this->json($msg));
			}
			
			
			
		}
		private function mark_already_exist()
			{
				$facult_id=$_POST['facult_id'];
				$student_id=$_POST['studen_id'];
				$quer1="select `ID` from `evaluation` where `Faculty_Code`='$facult_id' and `Student_id`='$student_id'";
				$res=mysql_query($quer1);
				if(mysql_num_rows($res)>0)
				{
					
					$msg=array("response_code"=>106 ,'response_desc'=>'Exist', 'message'=>'Already contain data');
					$this->response($this->json($msg),200);
				}
				else
				{
					$msg=array("response_code"=>100 ,'response_desc'=>'Unique', 'message'=>'not Exist');
					$this->response($this->json($msg),200);

				}
			}
			private function stun_under_faculty()
			{
				 $facult_id=$_POST['facult_id'];
				$quer="select `Name`,`Roll_no` from `student_registration` where `Guide`= '$facult_id' ";
				$qupr=mysql_query($quer);
				$stud_nam=array();
				while($res=mysql_fetch_assoc($qupr))
				{
					array_push($stud_nam,$res);
				}
				$this->response($this->json($stud_nam),200);
			}
			private function change_major()
			{
				$roll=$_POST['roll_no'];
				$topic=$_POST['topic'];
				$quer= "Update `student_registration` SET `topic`='$topic' where `Roll_No`='$roll'";
				if($qrrt=mysql_query($quer))
				{
					$rest=array("response_code"=>100,"response_desc"=>"Updated","message"=>"Updated Successfully");
					
					$this->response($this->json($rest),200);
				}
				else
				{
					$rest=array("response_code"=>101,"response_desc"=>"Sorry","message"=>"Updation Not Possible");
					
					$this->response($this->json($rest),200);
				}
				
			}
			private function out_of_faculty()
			{
				$fact=$_POST["fact_id"];
				$quer="SELECT `Roll_no`,`Name` FROM `student_registration` where `Guide`!='$fact'";
				$res=mysql_query($quer);
				$dat_ar=array();
				while($rest=mysql_fetch_assoc($res))
				{
					array_push($dat_ar,$rest);
				}
				$this->response($this->json($dat_ar),200);
			}
			private function student_evaluted()
			{
				$fact_id=$_POST["fact_id"];
				$quer="SELECT `Name`,`evaluation`.* FROM `evaluation`,`student_registration` where `Total`!=0 and `student_registration`.`Roll_no`=`evaluation`.`Student_id` and `evaluation`.`Faculty_Code`='$fact_id'";
				$res=mysql_query($quer);
				$respp=array();
				while($rtt=mysql_fetch_assoc($res))
				{
					array_push($respp,$rtt);
				}
				$this->response($this->json($respp),200);
		
			}
		private function faculty_drop_down()
		{
			$quer="select `Faculty_Code`,`Faculty_Name` from `faculty_registraion`";
			$qproc=mysql_query($quer);
			$qretr=array();
			while($ret=mysql_fetch_assoc($qproc))
			{
				array_push($qretr,$ret);
			}
			$this->response($this->json($qretr),200);
		}
		private function admin_change()
		{
			$adm_id=$_POST["admin_id"];
			$adm_pass=$_POST["password"];
			$quer="UPDATE `admin` SET `Password`='$adm_pass' WHERE `Admin_id`='$adm_id'";
			mysql_query($quer);
			$msg=array("response_code"=>100,'response_desc'=>"Updated",'meassage'=>"Password Updated");
			$this->response($this->json($msg),200);
		}
		private function adm_student_list()
		{
			$stud_id=$_POST["stud_id"];
			$quer="select `evaluation`.`Faculty_Code`,`faculty_registraion`.`Faculty_Name`,`evaluation`.`Student_id`,`student_registration`.`Name`,`student_registration`.`Branch`,`student_registration`.`topic`,`evaluation`.`Technical_Content`,`evaluation`.`Presentaion`,`Report_Writting`,`evaluation`.`planning_impl_rest` ,`evaluation`.`Total`,`evaluation`.`remark`,`type` from `evaluation`,`faculty_registraion`,`student_registration` where `evaluation`.`Student_id`='$stud_id' and `evaluation`.`Faculty_Code`=`faculty_registraion`.`Faculty_Code` and `student_registration`.`Roll_no`='$stud_id'";
			$res=mysql_query($quer);
			$dat_fi=array();
			while($rp=mysql_fetch_assoc($res))
			{
				array_push($dat_fi,$rp);
			}
			$this->response($this->json($dat_fi),200);
		
		}
		private function adm_login()
		{
			$adm_id=$_POST["admin_id"];
			$adm_pass=$_POST["password"];
			$qur="select * from `Admin` where `Password`='$adm_pass' and `Admin_id`='$adm_id'";
			$res=mysql_query($qur);
			$nrow=mysql_num_rows($res);
			
			if($nrow > 0)
			{
				$msg=array("responce_code"=>100,"response_desc"=>"Sucess","msg"=>"Login Successfull");	
				$this->response($this->json($msg),200);
			}
			else
			{
				$msg=array("responce_code"=>101,"response_desc"=>"Fail","msg"=>"Invalid Login Deatail");	
				$this->response($this->json($msg),200);
				
			}
		
	
			
		}
		private function avgstudent_marks()
		{
			//$roll=$_POST["roll_no"];
			$quer1="select `student_registration`.`Roll_no`,`Guide`,`evaluation`.`Faculty_Code`,AVG(`Total`) as avg_total from `student_registration`,`evaluation` where `student_registration`.`Roll_no`=`evaluation`.`Student_id` GROUP BY `evaluation`.`Student_id` ";
			$res=mysql_query($quer1);
			
			$sata=array();
			//$con=mysql_num_rows($res);
			while($rest=mysql_fetch_assoc($res))
			{
				array_push($sata,$rest);
			}
			//echo $con;
			$this->response($this->json($sata),200);
			
		}
		private function inter_marks()
		{
			$sid=$_POST["student_id"];
			$inte=$_POST["internal"];
			$typ=$_POST["typee"];
		//	$quer="select `Total` from `evaluation`";
		//	$
			$qur="UPDATE `evaluation` SET `internal`=$inte ,`total`=`total`+$inte,`type`='$typ' where `Student_id`='$sid'";
			if(mysql_query($qur))
			{
				$msg=array("responce_code"=>100,"response_desc"=>"Sucess","msg"=>"Updated");	
				$this->response($this->json($msg),200);
			}
			else
			{
				$msg=array("responce_code"=>101,"response_desc"=>"Fail","msg"=>"Not Able to Evaluate Mid Sem Evaluation required");	
				$this->response($this->json($msg),200);
			}
			
		}
		private function task_assgin()
		{
			$s_roll=$_POST["sroll"];
			$task_dec=$_POST["task_de"];
			$deadline=$_POST["dead"];
			//$asin_date=$_POST["a_date"];
			$quer="insert into task(`student_id`,`task`,`deadline`) values('$s_roll','$task_dec','$deadline')";
			if(mysql_query($quer))
			{
				$msg=array("response_code"=>100);
			}
			else
			{
				$msg=array("response_code"=>100);
			}
			$this->response($this->json($msg),200);
		}
		private function task_desc()
		{
			//`student_id`,`task`,`assaign_date`,`deadline`
			$roll_no=$_POST["rollno"];
			$quer="select * from task where `student_id`='$roll_no'";
			$querp=mysql_query($quer);
			$dat=array();
			while($res=mysql_fetch_assoc($querp))
			{
				array_push($dat,$res);
			}
			$this->response($this->json($dat),200);
		}
		private function adm_acc_faculty()
		{
			$fac=$_POST["fact_id"];
			$quer="select * from `evaluation` where `Faculty_Code`='$fac'";
			$querp=mysql_query($quer);
			$rest=array();
			while($res=mysql_fetch_assoc($querp))
			{
				array_push($rest,$res);
			}
			$this->response($this->json($rest),200);
		}
		private function adm_acc_student()
		{
			$stud=$_POST["stud_id"];
			$quer="select * from `evaluation` where `Student_id`='$stud' GROUP BY `Faculty_Code`";
			$querp=mysql_query($quer);
			$ress=array();
			while($rest=mysql_fetch_assoc($querp))
			{
				array_push($ress,$rest);
			}
			$this->response($this->json($ress),200);
		}
		private function adm_all_student()
		{
			$quer="select `Roll_no` FROM `student_registration`";
			$ress=mysql_query($quer);
			$rest=array();
			while($res=mysql_fetch_assoc($ress))
			{
				array_push($rest,$res);
			}
			$this->response($this->json($rest),200);
		}
		
		private function task_status()
		{
			$sroll=$_POST["student_roll"];
			$status=$_POST["student_status"];
			$task_id=$_POST["task_id"];
			
				$quer="UPDATE task SET status='$status' where `id`='$task_id' && `student_id`='$sroll'";
				if($pros_quer=mysql_query($quer))
				{
					$message=array("response_code"=>100);
				$this->response($this->json($message),200);
				}
				else
				{
					$message=array("response_code"=>101);
					$this->response($this->json($message),200);
				}
				
			
		}
		private function student_task()
		{
			$sroll=$_POST["student_roll"];
			$quer="select id from task where `student_id`='$sroll'";
			$quer=mysql_query($quer);
			$data=array();
			while($resp=mysql_fetch_assoc($quer))
			{
				array_push($data,$resp);
			}
			$this->response($this->json($data),200);
			
		}
	private function log_add()
	{
		$category=$_POST["category"];
		$action=$_POST["action"];
		$by=$_POST["by_w"];
		$quer="insert into dlog(`category`,`action`,`by_w`) values('$category','$action','$by')";
		mysql_query($quer);
		
	}
private function log_ret()
{
	$what=$_POST["category"];
	$quer=null;
	if($what!="All")
	{
		$quer="select * from `dlog` where `category`='$what'";
	}
	else
	{
		$quer="select * from `dlog`";
	}
	$res=array();
	$qu=mysql_query($quer);
	
	while($rt=mysql_fetch_assoc($qu))
	{
		array_push($res,$rt);
	}
	$this->response($this->json($res),200);
	
}
	private function retr_fac_name()
	{
		$fact_id=$_POST["fact_id"];
		$quer="select `Faculty_Name` from `faculty_registraion` where `Faculty_Code`='$fact_id'";
		$querp=mysql_query($quer);
		$rest=array();
		while($res=mysql_fetch_assoc($querp))
		{
			array_push($rest,$res);
		}
		
		$this->response($this->json($rest),200);
	}
	private function get_inter()
	{
		$sid=$_POST["stuid"];
		$querr="select `internal` as inters from `evaluation` where `Student_id`='$sid' Limit 1";
		$querp=mysql_query($querr);
		$retrr=array();
		while($rtt=mysql_fetch_assoc($querp))
		{
			
			array_push($retrr,$rtt);
		}
	//	array_push($retrr,'response_code'=>100);
		$this->response($this->json($retrr),200);
		
	}
	
//		public function guidevari($name,$)
private function json($data){
            if(is_array($data)){
                return json_encode($data);
            }
        }
		
    }
    
    // Initiiate Library
    
    $api = new API;
    $api->processApi();
	?>