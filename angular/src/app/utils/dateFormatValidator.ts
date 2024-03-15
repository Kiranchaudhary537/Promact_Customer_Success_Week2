import { AbstractControl, ValidatorFn } from '@angular/forms';

export function dateFormatValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const value = control.value;
    if (!value) {
      return null; // If the value is empty, don't perform validation
    }

    const dateFormat = /^\d{4}-\d{2}-\d{2}$/; // Regular expression to match "dd/mm/yyyy" format

    if (!dateFormat.test(value)) {
      return { invalidDateFormat: { value: control.value } };
    }

    const parts = value.split('-');
    const year = parseInt(parts[0], 10);
    const month = parseInt(parts[1], 10)-1 // Month is zero-based
    const day = parseInt(parts[2], 10);

    const date = new Date(year, month, day); // Create a Date object with parsed components

    console.log(day,month,year);
    console.log(date);
    // Check if the day is within the range for the month and year
    if (date.getFullYear() !== year || date.getMonth() !== month || date.getDate() !== day) {
      return { invalidDate: { value: control.value } };
    }

    return null;
  };
}

export function convertToDate(dateString) {
  // Parse the original date string
//   const date = new Date(dateString);/
const DateTime = dateString.split('T')[0];

  // Extract day, month, and year components
  const parts = DateTime.split('-');

  // Rearrange the components into "yyyy/mm/dd" format
  const formattedDateString = `${parts[0]}-${parts[1]}-${parts[2]}`;


  return formattedDateString;
}
