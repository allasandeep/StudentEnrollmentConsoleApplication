/*********************************************************************************************************
 *                                                                                                       *
 *  CSCI 473/504							Assignment 1								 Fall 2018       *                                           
 *																										 *
 *  Programmer's: Sandeep Alla (z1821331)   *  
 *																										 *
 *  Date Due  : September 13th, 2018			File :	Course.cs					     				 *                          
 *																										 *
 *  Purpose   : To write a program that allows user to Enroll into available Courses. This file contains *
 *            	a class for Course.															             *
 *********************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1
{
	public class Course : IComparable //Here Course class implements IComparable interface
	{
		public string deptCode, sectionNum; // Declaring variables of string type and is public
		public uint courseNum; //Declaring variable of integer type and is public
		public ushort creditHours, numOfStudentsCurrentlyEnrolled, maxCapacityOfEnrollment;//Declaring variables of ushort type and is public
		public List<uint> zID = new List<uint>();

		public String DeptCode // Name method
		{
			get { return deptCode; } //Get property
			set { deptCode = value; } // Set property
		}

		public String SectionNum // Name method
		{
			get { return sectionNum; } //Get property
			set { sectionNum = value; } // Set property
		}

		public uint CourseNum // Name method
		{
			get { return courseNum; } //Get property
			set { courseNum = value; } // Set property
		}

		public ushort CreditHours // Name method
		{
			get { return creditHours; } //Get property
			set { creditHours = value; } // Set property
		}

		public ushort NumOfStudentsCurrentlyEnrolled // Name method
		{
			get { return numOfStudentsCurrentlyEnrolled; } //Get property
			set { numOfStudentsCurrentlyEnrolled = value; } // Set property
		}

		public ushort MaxCapacityOfEnrollment // Name method
		{
			get { return maxCapacityOfEnrollment; } //Get property
			set { maxCapacityOfEnrollment = value; } // Set property
		}

		public List<uint> ZID // Name method
		{
			get { return zID; } //Get property
			set { zID = value; } // Set property
		}


		public Course() // Default Constructor for Course class
		{
			this.deptCode = null;
			this.sectionNum = null;
			this.courseNum = 0;
			this.creditHours = 0;
			this.maxCapacityOfEnrollment = 0;
			this.numOfStudentsCurrentlyEnrolled = 0;
			this.zID = new List<uint>();
		}

		//Alternate Constructor for Course class
		public Course(string deptCode, string sectionNum, uint courseNum, ushort creditHours, ushort maxCapacityOfEnrollment)
		{
			this.deptCode = deptCode;
			this.sectionNum = sectionNum;
			this.courseNum = courseNum;
			this.creditHours = creditHours;
			this.maxCapacityOfEnrollment = maxCapacityOfEnrollment;
			this.zID = new List<uint>();
		}

		//Comparable method for Sorting
		public int CompareTo(object alpha)
		{
			Course course = (Course)alpha;
			if ((this.courseNum).Equals(course.CourseNum))
				return 0;
			else if (((this.courseNum).CompareTo(course.CourseNum)) > 0 )
				return 1;
			else return -1;

		}

		//Print Roster method prints the currently enrolled student details for a particular course
		public void PrintRoster()
		{
			if (ZID.Count == 0) // When there are no students in a course prints message
			{
				Console.BackgroundColor = ConsoleColor.White;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.WriteLine("There are currently no Students Enrolled in " + DeptCode + " " + CourseNum + "-" + SectionNum + ".");
				Console.WriteLine();
				Console.ResetColor();
			}
			else
			{
				Console.WriteLine("----------------------------------------------------------------------------");
				Console.WriteLine("Course : ".PadLeft(30) + DeptCode + " " + CourseNum + "-" + SectionNum + "<" + NumOfStudentsCurrentlyEnrolled + "/" + MaxCapacityOfEnrollment + ">");
				Console.WriteLine("----------------------------------------------------------------------------");
				foreach (var zid in ZID)//Looping through the ZIDS
				{
					foreach ( var sPool in Program.studentPool) // Looping through studentPool list
					{
						if (sPool.ZID.Equals(zid))
						{
							String firstname = sPool.FirstName;
							String lastname = sPool.LastName;
							String major = sPool.Major;
							//Displaying the Student details who are currently enrolled in this instance of course
							Console.WriteLine(Convert.ToString(zid) + firstname.PadLeft(20) + "," + lastname.PadRight(20) + major);
							break;
						}
					}

				}
				
				Console.WriteLine();
			}
		}


		//Overrides the string
		public override string ToString()
		{
			return base.ToString();
		}


	}
}
