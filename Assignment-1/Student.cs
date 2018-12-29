/*********************************************************************************************************
 *                                                                                                       *
 *  CSCI 473/504							Assignment 1								 Fall 2018       *                                           
 *																										 *
 *  Programmer's: Sandeep Alla (z1821331) *  
 *																										 *
 *  Date Due  : September 13th, 2018			File :	Course.cs					     				 *                          
 *																										 *
 *  Purpose   : To write a program that allows user to Enroll into available Courses. This file contains *
 *            	a class for Student.															         *
 *********************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1
{
	public enum AcademicYear { Freshman = 0, Sophomore, Junior, Senior, PostBacc }; // Decalring Academic year of type enum

	public class Student : IComparable //Here Student class implements IComparable interface
	{
		//Decalring variables
		private readonly uint zID;
		private string firstName,
				lastName,
				major;
		private readonly AcademicYear academicYear;
		private float gPA;
		private ushort creditHoursEnrolled;


		public uint ZID // Name method
		{
			get { return zID; } //Get property
			//No set property because it is readonly
		}

		public String FirstName // Name method
		{
			get { return firstName; } //Get property
			set { firstName = value; } // Set property
		}

		public String LastName // Name method
		{
			get { return lastName; } //Get property
			set { lastName = value; } // Set property
		}

		public String Major // Name method
		{
			get { return major; } //Get property
			set { major = value; } // Set property
		}

		public AcademicYear CurrentAcademicYear
		{
			get { return academicYear; }
		}

		public float GPA // Name method
		{
			get { return gPA; } //Get property
			set { gPA = value; } // Set property
		}

		public ushort CreditHoursEnrolled // Name method
		{
			get { return creditHoursEnrolled; } //Get property
			set { creditHoursEnrolled = value; } // Set property
		}

		//Default Constructor
		public Student()
		{
			this.firstName = null;
			this.lastName = null;
			this.major = null;
			this.gPA = 0;
			this.creditHoursEnrolled = 0;
		}

		//Alternate Constructor
		public Student(uint zID, string firstName, string lastName, string major, string academicYear, float gPA)
		{
			this.zID = zID;
			this.firstName = firstName;
			this.lastName = lastName;
			this.major = major;
			this.academicYear = (AcademicYear)Enum.Parse(typeof(AcademicYear), academicYear);
			this.gPA = gPA;
		}

		//Icomparable method for sorting
		public int CompareTo(object alpha)
		{
			Student student = (Student)alpha;
			if ((this.zID).Equals(student.ZID))
				return 0;
			else if (((this.zID).CompareTo(student.ZID)) > 0)
				return 1;
			else return -1;
		}

		//Enroll method to enroll students in to available course
		public int Enroll(Course newCourse)
		{
			int errorCode = 0;
			if (newCourse.numOfStudentsCurrentlyEnrolled >= newCourse.maxCapacityOfEnrollment)
				errorCode = 5; // Returns 5 if the number of currently enrolled students exceed the maximum capacity of the course
			else if (newCourse.creditHours + creditHoursEnrolled > 18)
				errorCode = 15; // Returns 15 if the credits hours of the student exceed the maximum allowed
			else
			{
				
				bool result = false;
				for (int i = 0; i < newCourse.ZID.Count; i++)
				{
					if ((newCourse.ZID[i]).Equals(ZID))
					{
						errorCode = 10; // Returns 10 if the student is already enrolled into this course
						result = true;
					}
				}

				if (result != true)
				{
					newCourse.ZID.Add(ZID); // Adding the Zid into the Zid list of course class
					newCourse.numOfStudentsCurrentlyEnrolled++; // increasing the number of students enrolled for that instance of course
					CreditHoursEnrolled = (ushort)(CreditHoursEnrolled + newCourse.CreditHours);
					errorCode = 0; // Returns 0 if the student is successfully enrolled into the course
				}
				
			}

			return errorCode;
		}

		//Drop Method is to drop a student from a particular course.
		public int Drop(Course newCourse)
		{
			int errorCode = 20;
			for (int i = 0; i < newCourse.ZID.Count; i++)
			{
				if ((newCourse.ZID[i]).Equals(ZID))
				{
					newCourse.ZID.RemoveAt(i); //Removing the students from the ZID list
					newCourse.numOfStudentsCurrentlyEnrolled--; //Decrementing the number of enrolled students value
					errorCode = 0; // Returns 0 if the student is successfull dropped from the course
				}				
			}

			return errorCode;
		}


		public override string ToString()
		{
			return base.ToString();
		}


	}
}
