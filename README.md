
# Display-If

The Display-If component is used to toggle the display of content based on the value of a models input.
You can set which form input is the toggle and then define which elements are toggled by the form input by which value.

There are two tag helpers:

## Display If Toggle

This tag helper should be used on the `input` or `select` elements that will toggle the visibility of other elements based on it's value.

The `display-if-toggle` property takes a `ModelExpression` value (meaning that it must be a property on a model).

Example:

```html
<input asp-for="HasAnyPets"
    display-if-toggle="HasAnyPets">
```

## display-if

This tag helper should be used on the element (i.e. `div`) that you want to hide or show based on the value of the models property.

The `display-if` tag helper has the following properties:

- `display-if`: This property is a `ModelExpression` that is used to determine the value from the input that has the same `ModelExpression` set for `display-if-toggle` (i.e. `HasAnyPets`).
- `display-if-value`: This property is the value that the `display-if-toggle` input must have for this element to be visible. If the input value does not match the value provided to this property the element will be hidden.
- `display-if-not-value`: Does the opposite of the `display-if-value` property. It will display if the value is not equal to this property.
- `display-if-has-value`: Boolean value that only checks that the `display-if` property has a value and will display if it does.
- `display-if-client-evaluate`: A client side function used to determine if the content should be displayed or not.

***Note:*** The Client Evaluate function does not run on first page load only when the inputs are changed does it trigger. Therefor it is common to use a custom property on your model that contains the same display logic as the client function. So you have a client and server side version to display or not.

Example:

### display-if-value

```html

<label>Do you have any pets?</label>
<input asp-for="HasAnyPets" display-if-toggle="HasAnyPets" />

<div class="form-row"
    display-if="HasAnyPets"
    display-if-value="true">
    User has pets...
</div>
```

### display-if-has-value

```html

<label>Please select all types of pets below.</label>
<select asp-for="PetTypes" display-if-toggle="PetTypes">
    <option value="dog">Dog</option>
    <option value="cat">Cat</option>
    <option value="other">Other</option>
</select>

<div class="form-row"
    display-if="PetTypes"
    display-if-has-value="true">
    Here are your pet types: ...
</div>
```

### display-if-not-value

```html

<label>What is your age range?</label>
<select asp-for="AgeRange" display-if-toggle="AgeRange">
    <option value="18-25">18-25</option>
    <option value="25-36">25-36</option>
    <option value="36+">36+</option>
    <option value="other">other</option>
</select>

<div class="form-row"
    display-if="PetTypes"
    display-if-not-value="other">
    You are in an expected age group.
</div>

<div class="form-row"
    display-if="PetTypes"
    display-if-value="other">
    You don't fit into any expected category. Goodbye.
</div>
```

### display-if-client-evaluate

```csharp
public class ScheduleModel
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public bool ValidDateRange
    {
        get
        {
            return StartDate < EndDate;
        }
    }
}
```

```html

<!-- Here the toggle is the same for both inputs as the display-if is dependent upon both values. -->
<label>Start Date</label>
<input asp-for="StartDate" display-if-toggle="ValidDateRange" />

<label>End Date</label>
<input asp-for="EndDate" display-if-toggle="ValidDateRange" />

<!-- The Client Evaluate function should be used in addition to the display-if-value as the first page load will use the Model Property. Each change to the input will then trigger the client function. -->
<div class="form-row"
    display-if="ValidDateRange"
    display-if-value="false"
    display-if-client-evaluate="validateDates">
    You are in an expected age group.
</div>

```

### Troubleshooting

If there issues check the following three files:

- `[ProjectDirectory]/TagHelpers/DisplayIfTagHelper.cs`
- `[ProjectDirectory]/TagHelpers/DisplayIfToggleTagHelper.cs`
- `[ProjectDirectory]/_scripts/components/display-if.js`