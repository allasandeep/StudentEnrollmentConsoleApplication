/*********************************************************************************************************
 *                                                                                                       *
 *  CSCI 473/504							Assignment 1								 Fall 2018       *                                           
 *																										 *
 *  Programmer's: Sandeep Alla (z1821331) Gayathri Sanikommu (z1822939) Venkata SuryaVamsi (z1855404)    *  
 *																										 *
 *  Date Due  : September 13th, 2018			File :	Course.cs					     				 *                          
 *																										 *
 *  Purpose   : To write a program that allows user to Enroll into available Courses. This file contains *
 *            	logic for main menu.														             *
 *********************************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1
{
	class Program 
	{
		//Global Variables
		public static List<Student> studentPool = new List<Student>(); // Initializing a list of type Student 
		public static List<Course> coursePool = new List<Course>(); // Initializing a list of type Course
	
		public static void read() // read method to get data from a file
		{
			//Variables to store the contents read from the input files
			uint newZID;
			string newFirstName;
			string newLastName;
			string newMajor;
			float newGPA;
			string newAcademicYear;
			string newDeptCode, newSectionNum;
			uint newCourseNum;
			ushort newCreditHours, newMaxCapacityOfEnrollment;

			try
			{
				//Reading the file
				using (StreamReader inFile = new StreamReader(Directory.GetCurrentDirectory() + "\\2188_a1_input01.txt")) // StreamReader is used to read data from a text file
				{
					string data = inFile.ReadLine(); // Reading the firstline and storing it in string variable data
					while (data != null) // Loop until there is nothing to read from the file
					{
						string[] result = data.Split(','); //Splitting the data seperated by ','
						newZID = Convert.ToUInt32(result[0]);
						newFirstName = result[1];
						newLastName = result[2];
						newMajor = result[3];
						newAcademicYear = result[4];
						newGPA = float.Parse(result[5]);
						Student student = new Student(newZID, newFirstName, newLastName, newMajor, newAcademicYear, newGPA); // Creating an object of the class Student
						studentPool.Add(student); // Adding objects into a list
						data = inFile.ReadLine();
					}

					inFile.Close(); // Closing the file
				}
			}
			catch(Exception e) // Catchs any exceptions that are raised during the file read
			{
				Console.WriteLine("The file could not be read:"); // Error Message
				Console.WriteLine(e.Message);
			}

			try
			{
				using (StreamReader inFile = new StreamReader(Directory.GetCurrentDirectory() + "\\2188_a1_input02.txt")) // StreamReader is used to read data from a text file
				{
					string data = inFile.ReadLine(); // Reading the firstline and storing it in string variable data
					while (data != null) // Loop until there is nothing to read from the file
					{
						string[] result = data.Split(','); //Splitting the data seperated by ','
						newDeptCode = result[0];
						newCourseNum = Convert.ToUInt32(result[1]);
						newSectionNum = result[2];
						newCreditHours = ushort.Parse(result[3]);
						newMaxCapacityOfEnrollment = ushort.Parse(result[4]);
						Course course= new Course(newDeptCode, newSectionNum, newCourseNum, newCreditHours, newMaxCapacityOfEnrollment); // Creating an object of the class Student
						coursePool.Add(course); // Adding objects into a list
						data = inFile.ReadLine();
					}
					inFile.Close();
				}
			}
			catch(Exception e) // Catchs any exceptions that are raised during the file read
			{
				Console.WriteLine("The file could not be read:"); // Error Message
				Console.WriteLine(e.Message);
			}


		}

		public static void Main(string[] args)
		{
			Program.read(); // Calling the read method from the Program class
			Char s;
			int num = 0;
			while (num == 0)  //checking for condition
			{                 //printing a menu based on the question
				Console.WriteLine("_______________________MAIN MENU_______________________");
				Console.WriteLine("-------------------------------------------------------");
				Console.WriteLine("Please choose from the following options:");
				Console.WriteLine();
				Console.WriteLine("A. Print Student List <All>.");
				Console.WriteLine("B. Print Student List <Major>.");
				Console.WriteLine("C. Print Student List <Academic Year>");
				Console.WriteLine("D. Print Course List.");
				Console.WriteLine("E. Print Course Roster.");
				Console.WriteLine("F. Enroll Student.");
				Console.WriteLine("G. Drop Student.");
				Console.WriteLine("H. Quit Application");				
				Console.WriteLine("-------------------------------------------------------");
				Console.Write("Enter your Option :");
				try
				{
					s = Convert.ToChar(Console.ReadLine()); // User types a choice of his own to perform an action
					Console.WriteLine("-------------------------------------------------------");
					if (s == 'A' || s == 'a' || s == 'B' || s == 'b' || s == 'C' || s == 'c' || s == 'D' || s == 'd' || s == 'E' || s == 'e' || s == 'F' || s == 'f' || s == 'G' || s == 'g' || s == 'h' || s == 'H' ) 
					{
						switch (s) // Switch case
						{
							case 'A':
							case 'a': // Displays all the students details available								
								studentPool.Sort();					
								Console.BackgroundColor = ConsoleColor.Yellow;
								Console.ForegroundColor = ConsoleColor.Black;
								Console.WriteLine("Student Report in sorted order");
								Console.ResetColor();
								Console.WriteLine("Student List <All Students>");
								Console.WriteLine("ZID".PadRight(20) + "First Name,Last Name".PadRight(20) + "Major".PadLeft(20) + "Academic Year".PadLeft(20) + "GPA".PadLeft(10));
								Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
								foreach (var sPool in studentPool) //Loops through the studentPool list to display each an every student data
								{
									Console.WriteLine("Z" + (Convert.ToString(sPool.ZID)) + " --" + (sPool.FirstName).PadLeft(20) + "," + (sPool.LastName).PadRight(20) + "[" + Convert.ToString((sPool.CurrentAcademicYear)).PadLeft(10) + "] " + " <" + (sPool.Major) + "> " + " | "+ Convert.ToString((sPool.GPA).ToString(".000")).PadRight(5) + " |");
								} 
								if(studentPool.Count == 0)
								{
									Console.BackgroundColor = ConsoleColor.White;
									Console.ForegroundColor = ConsoleColor.Black;
									Console.WriteLine("There are no Students");
									Console.ResetColor();
								}
								Console.WriteLine();
								break; // break statement
							case 'B':
							case 'b': // Displays student details based on the Major they are studying
								Console.Write("Which Major List you would like printed :"); // User enters the Major
								String input = Console.ReadLine();
								bool result = false;
								studentPool.Sort(); // Sorts the list
								foreach (var sPool in studentPool) //Loops through the studentPool list to display each an every student data
								{
									//Checking if the major entered by the user exists.
									if ((((sPool.Major).Replace(" ", String.Empty)).ToLower()).Equals(((input).Replace(" ", String.Empty)).ToLower()))
										result = true;									
								}
								if(result == true)
								{
									Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
									Console.WriteLine("ZID".PadRight(20) + "First Name,Last Name".PadRight(20) + "Major".PadLeft(20) + "Academic Year".PadLeft(20) + "GPA".PadLeft(10));
									Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
								}
								else
								{
									Console.BackgroundColor = ConsoleColor.White;
									Console.ForegroundColor = ConsoleColor.Black;
									Console.WriteLine("There are no students majoring in '" + input +"' \\ There is no such type of major");
									Console.ResetColor();
								}
								foreach ( var sPool in studentPool) //Loops through the studentPool list to display each an every student data
								{

									if ((((sPool.Major).Replace(" ", String.Empty)).ToLower()).Equals(((input).Replace(" ", String.Empty)).ToLower()))
									{
										Console.WriteLine("Z" + (Convert.ToString(sPool.ZID)) + " --" + (sPool.FirstName).PadLeft(20) + "," + (sPool.LastName).PadRight(20) + "[" + Convert.ToString((sPool.CurrentAcademicYear)).PadLeft(10) + "] " + " <" + (sPool.Major) + "> " + " | " + Convert.ToString((sPool.GPA).ToString(".000")).PadRight(5) + " |");

									}
								}
								Console.WriteLine();
								break;
							case 'c':
							case 'C': //Displays the student details based on the Academic Year entered by the user
								Console.Write("Enter the Academic Year you wanted to see from < Freshman, Sophomore, Junior, Senior, PostBacc> :");
								input  = Console.ReadLine();
								result = false;
								studentPool.Sort(); // Sorts
								foreach ( var sPool in studentPool) //Loops through the studentPool list to display each an every student data
								{
									if (((Convert.ToString(sPool.CurrentAcademicYear).Replace(" ", String.Empty)).ToLower()).Equals(((input).Replace(" ", String.Empty)).ToLower()))
										result = true; //If the user entered input matchs with the data present in the file then make result true
								}
								if (result == true)
								{
									Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
									Console.WriteLine("ZID".PadRight(20) + "First Name,Last Name".PadRight(20) + "Major".PadLeft(20) + "Academic Year".PadLeft(20) + "GPA".PadLeft(10));
									Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
								}
								else
								{
									Console.BackgroundColor = ConsoleColor.White;
									Console.ForegroundColor = ConsoleColor.Black;
									Console.WriteLine("The Academic Year doesn't exists, Please try entering a valid Academic Year.");
									Console.ResetColor();
								}
								

								foreach (var sPool in studentPool) //Loops through the studentPool list to display each an every student data
								{

									if (((Convert.ToString(sPool.CurrentAcademicYear).Replace(" ", String.Empty)).ToLower()).Equals(((input).Replace(" ", String.Empty)).ToLower()))
									{
										Console.WriteLine("Z" + (Convert.ToString(sPool.ZID)) + " --" + (sPool.FirstName).PadLeft(20) + "," + (sPool.LastName).PadRight(20) + "[" + Convert.ToString((sPool.CurrentAcademicYear)).PadLeft(10) + "] " + " <" + (sPool.Major) + "> " + " | " + Convert.ToString((sPool.GPA).ToString(".000")).PadRight(5) + " |");
										result = true;
									}
								}
								Console.WriteLine();
								break;
							case 'd':
							case 'D': // Displays the list of courses available								
								coursePool.Sort(); // Sorts
								Console.WriteLine("Course List <All Courses>");
								Console.WriteLine("----------------------------");
								foreach ( var cPool in coursePool) //Loops through the coursePool list to display each an every Course details
								{
									Console.WriteLine((Convert.ToString(cPool.DeptCode)).PadRight(5) + cPool.CourseNum + "-" + cPool.SectionNum + "  <" + cPool.NumOfStudentsCurrentlyEnrolled  + "/" + cPool.MaxCapacityOfEnrollment + ">");
								}
								break; // break statement
							case 'e':
							case 'E': // Displays the course Roster for the course entered by the user
								Console.Write("Which Course Roster would you like printed");
								Console.Write("<DEPT COURSE_NUM-SECTION_NUM> :");
								String courseInput = Console.ReadLine();
								result = false;
								foreach ( var cPool in coursePool) //Loops through the coursePool list to display each an every Course details
								{
									String Course = Convert.ToString(cPool.DeptCode) + " " + Convert.ToString(cPool.CourseNum) + "-" + Convert.ToString(cPool.SectionNum);
									if (Course.Equals(courseInput)) // If the user entered user exists
									{
										cPool.PrintRoster(); // Calls the PrintRoster method from Course class
										result = true; 
										break;
									}
								}

								Console.BackgroundColor = ConsoleColor.White;
								Console.ForegroundColor = ConsoleColor.Black;
								if (result == false) // If the user entered course doesn't exists
								{
									Console.WriteLine("There is no such type of Course, Please enter a correct course ");
								}
								Console.ResetColor();
								Console.WriteLine();
								break;
							case 'f':
							case 'F': // To enroll a student into a particular course entered by the user
								Console.Write("Which Course will this student be enrolled into?");
								Console.Write("<DEPT COURSE_NUM-SECTION_NUM>:");
								courseInput = Console.ReadLine();
								Console.Write("Please enter the Z-ID <omitting the Z character> of the student you would like to enroll into a course :");
								String studentInput = Console.ReadLine();	
								result = false;
								bool result2 = false;
								foreach (var sPool in studentPool) // Loops through the studentPool
								{							
									if (Convert.ToString(sPool.ZID).Equals(studentInput)) // If the zID of the student exists in the file then
									{
										result2 = true;
										foreach ( var cPool in coursePool) // Loops through the coursePool
										{
											String Course = Convert.ToString(cPool.DeptCode) + " " + Convert.ToString(cPool.CourseNum) + "-" + Convert.ToString(cPool.SectionNum);
											if (Course.Equals(courseInput)) // If the Course entered by the user exists in the course file then
											{
												int errorCode = sPool.Enroll(cPool); // Calling the Enroll Method from the Student class
												//Displaying messages based on the error code returned
												if (errorCode == 5)
													Console.WriteLine("Failed to Enroll the Student Z" + sPool.ZID + ", because the Capacity of the course " + Course + " has already reached Maximum");
												else if (errorCode == 10)
													Console.WriteLine("Failed to Enroll the Student Z" + sPool.ZID + ", because he is already enrolled into the course " + Course);
												else if (errorCode == 15)
													Console.WriteLine("Failed to Enroll the student Z" + sPool.ZID + ", because he is exceeding his maximium allowed credit hours");
												else if (errorCode == 0)
													Console.WriteLine("The Student Z" + sPool.ZID + " has been successfully enrolled into the course " + Course +"\nTotal Credit Hours Erolled : " + sPool.CreditHoursEnrolled);
												result = true;
												break;
											}
											
										}										
									}
								}
								Console.BackgroundColor = ConsoleColor.White;
								Console.ForegroundColor = ConsoleColor.Black;
								if (result2 == false) // If the Zid Entered by the user doesn't exists
									Console.WriteLine("There is no student with the given ZID");
								if (result == false ) //If the Course entered by the user doesn't exists
									Console.WriteLine("Your entered an incorrect course \\ The course you entered doesn't exists");

								Console.ResetColor();
								Console.WriteLine();
								break;
							case 'G':
							case 'g': // To drop a student into a particular course entered by the user
								Console.Write("Enter the Course from which you want to drop the student:");
								Console.Write("<DEPT COURSE_NUM-SECTION_NUM>:");
								courseInput = Console.ReadLine();
								Console.Write("Enter the ZID of the student to be dropped <Omitting 'Z' character> :");
								studentInput = Console.ReadLine();
								result = false;
								result2 = false;
								foreach ( var sPool in studentPool) // Loops through the studentPool
								{
									if (Convert.ToString(sPool.ZID).Equals(studentInput)) // If the zID of the student exists in the file then
									{
										result2 = true;
										foreach ( var cPool in coursePool)
										{
											String Course = Convert.ToString(cPool.DeptCode) + " " + Convert.ToString(cPool.CourseNum) + "-" + Convert.ToString(cPool.SectionNum);
											if (Course.Equals(courseInput)) // If the Course entered by the user exists in the course file then
											{
												int errorCode = sPool.Drop(cPool); // Calling the Drop method from the Student class
												//Displaying messages based on the error code returned
												if (errorCode == 0)
												{
													Console.WriteLine("The student Z" + sPool.ZID + " has been successfully dropped from the course " + Course);
													Console.WriteLine("Total Credit Hours Enrolled by Z" + sPool.ZID + " :" + sPool.CreditHoursEnrolled);
												}
												else if (errorCode == 20)
													Console.WriteLine("Failed to drop the student Z" + sPool.ZID + ", because he isn't currently enrolled into the course " + Course);
												
												result = true;
												break;
											}

										}
									}
								}
								Console.BackgroundColor = ConsoleColor.White;
								Console.ForegroundColor = ConsoleColor.Black;
								if (result2 == false) // If the Zid Entered by the user doesn't exists
									Console.WriteLine("There is no student with the given ZID");
								if (result == false) //If the Course entered by the user doesn't exists
									Console.WriteLine("Your entered an incorrect course \\ The course you entered doesn't exists");
								Console.ResetColor();
								Console.WriteLine();
								break;
							case 'h':
							case 'H':
								Console.WriteLine("Quit"); //Quit
								num = 1;
								break;
							default:
								break;
						}
					}
					else
					{
						Console.BackgroundColor = ConsoleColor.White;
						Console.ForegroundColor = ConsoleColor.Black;
						Console.WriteLine("\nEnter a Valid option between A to G\n");
						Console.ResetColor();
					}
				}				
				catch (Exception e)
				{
					Console.BackgroundColor = ConsoleColor.White;
					Console.ForegroundColor = ConsoleColor.Black;
					Console.WriteLine("\nError Message :\n");
					Console.WriteLine(e.Message);
					Console.ResetColor();
				}
	        }
			Console.WriteLine("Press Enter To Exit");
			Console.ReadLine();


		}

	}	
	   	
}
