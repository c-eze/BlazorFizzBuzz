﻿using System.ComponentModel.DataAnnotations;
	
namespace BlazorFizzBuzz.Models
{
	public class FizzBuzz
	{
		[Required(ErrorMessage = "Please enter a Fizz value")]
		[Range(1, 100, ErrorMessage = "Value must be between 1 and 100")]
		public int FizzValue { get; set; } = 3;

		[Required(ErrorMessage = "Please enter a Buzz value")]
		[Range(1, 100, ErrorMessage = "Value must be between 1 and 100")]
		public int BuzzValue { get; set; } = 5;

		[Required(ErrorMessage = "Please enter a Stop value")]
		[Range(1, 1000, ErrorMessage = "Stop value must be between 1 and 1000")]
		public int StopValue { get; set; } = 100;
	}
}
