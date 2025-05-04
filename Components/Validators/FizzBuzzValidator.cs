using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using BlazorFizzBuzz.Models;
using BlazorFizzBuzz.Components.Pages;

namespace BlazorFizzBuzz.Components.Validators
{
	public class FizzBuzzValidator : ComponentBase
	{
		private ValidationMessageStore validationMessageStore;

		[CascadingParameter]
		private EditContext CurrentEditContext { get; set; }

		protected override void OnInitialized()
		{
			if (CurrentEditContext == null)
			{
				throw new InvalidOperationException($"{nameof(FizzBuzzValidator)} requires a cascading parameter of type {nameof(EditContext)}. For example, you can use {nameof(FizzBuzzValidator)} inside an {nameof(EditForm)}.");
			}

			validationMessageStore = new(CurrentEditContext);

			//Attach methods to validation events.
			CurrentEditContext.OnFieldChanged += (s,e) => ValidateField(e.FieldIdentifier);
			CurrentEditContext.OnValidationRequested += (s, e) => ValidateAllFields();
		}

		private void ValidateField(FieldIdentifier fieldIdentifier)
		{
			var fizzbuzz = CurrentEditContext.Model as FizzBuzz ?? throw new InvalidOperationException($"{nameof(FizzBuzzValidator)} requires a model of type FizzBuzz");

			//Clear previous errors for the field.
			validationMessageStore.Clear(fieldIdentifier);

			//Validate the field.
			if(fieldIdentifier.FieldName == nameof(FizzBuzz.FizzValue))
			{
				if(fizzbuzz.FizzValue >= fizzbuzz.BuzzValue)
				{
					validationMessageStore.Add(fieldIdentifier, "The Fizz value must be less than the buzz value");
				}
			}
			else if (fieldIdentifier.FieldName == nameof(FizzBuzz.BuzzValue))
			{
				if (fizzbuzz.BuzzValue <= fizzbuzz.FizzValue)
				{
					validationMessageStore.Add(fieldIdentifier, "The Buzz value must be greater than the fizz value");
				}
			}
			else if (fieldIdentifier.FieldName == nameof(FizzBuzz.StopValue))
			{
				int requiredStopValue = fizzbuzz.FizzValue * fizzbuzz.BuzzValue;
				if (fizzbuzz.StopValue < requiredStopValue)
				{
					validationMessageStore.Add(fieldIdentifier, $"The Stop value must be greater than or equal to {requiredStopValue}");
				}
			}
		}

		private void ValidateAllFields()
		{
			var fizzbuzz = CurrentEditContext.Model as FizzBuzz ?? throw new InvalidOperationException($"{nameof(FizzBuzzValidator)} requires a model of type FizzBuzz");

			//Clear all validation errors.
			validationMessageStore.Clear();

			//Validate all fields
			ValidateField(new FieldIdentifier(fizzbuzz, nameof(FizzBuzz.FizzValue)));
			ValidateField(new FieldIdentifier(fizzbuzz, nameof(FizzBuzz.BuzzValue)));
			ValidateField(new FieldIdentifier(fizzbuzz, nameof(FizzBuzz.StopValue)));

			CurrentEditContext.NotifyValidationStateChanged();
		}
	}
}
