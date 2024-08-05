# FinancePad(AKA överkomplicerad lowtech miniräknare)

### About:
Notepad but for calculating home finances or smth. A richTextBox with a badly coded _"scripting language"_.

### The "scripting language" content:
- Label
  - A block of code with a starting value of 0 (if not modified).
  - syntax:
    ```
    labelName: -create a label.
    labelName (otherlabel): -create a label that inherits the value of another label.
    ```
- variables
  - can be created both inside and outside labels.
  - syntax:
    ```
    {value} {(variableName)}
    ```
  - if created inside a label variables need an operator.
- "+" and "-"
  - used to add or remove from a labels value.
  - syntax:
  	```
		+{value} {(variableName)} -all values are stored as a variable.
		```
   	```
   	+{variableName} -if variable created outside label.
		```
- "*"
  - used to multiply a variable x amount of times inside a label.
  - syntax:
    ```
    {(variableName)} *{amount}
    ```
- Template keyword
  - used to create a block of code that will be applied to all labels automatically.
  - syntax:
  ```
  Template:
	```
  
### Example code:
```
100 (aVariable)

Template:
+(aVariable)

labelOne:
-20 (anotherVariable)

result:
labelOne: 80
```
  
