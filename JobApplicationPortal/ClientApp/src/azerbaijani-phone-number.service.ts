import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AzerbaijaniPhoneNumberService {
  // Define the regex patterns as constants
  private azerbaijaniPhoneNumberPrefixRegex: RegExp = /^(50|51|55|70|77|99|10)\d*$/;
  private azerbaijaniPhoneNumberBaseRegex: RegExp = /^(50|51|55|70|77|99|10)(?![01])\d*$/;
  private azerbaijaniPhoneNumberRegex: RegExp = /^(50|51|55|70|77|99|10)(?![01])\d{7}$/;

  // Method to check if a phone number has a valid Azerbaijani prefix
  checkIfValidAzerbaijaniPhoneNumberPrefix(phoneNumber: string): boolean {
    return this.azerbaijaniPhoneNumberPrefixRegex.test(phoneNumber);
  }

  // Method to check if the base part of the Azerbaijani phone number is valid
  checkIfValidAzerbaijaniPhoneNumberBase(phoneNumber: string): boolean {
    return this.azerbaijaniPhoneNumberBaseRegex.test(phoneNumber);
  }

  // Method to check if the phone number is a valid Azerbaijani number
  checkIfValidAzerbaijaniPhoneNumber(phoneNumber: string): boolean {
    return this.azerbaijaniPhoneNumberRegex.test(phoneNumber);
  }
}
