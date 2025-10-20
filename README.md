# Unity Custom Inspector Attributes

This package provides a collection of custom attributes and property drawers for Unity Editor, designed to enhance the inspector experience and streamline your workflow.

## Features

-   Conditional display, enable, or disable of fields in the inspector
-   Read-only and required field indicators
-   Custom help boxes and separators for better organization
-   Minimal boilerplate for adding advanced inspector logic

## Included Attributes

### `DisableIFAttribute`

Disables a field in the inspector if a specified boolean field is `true`.

```csharp
[DisableIF("isDisabled")]
public int myValue;
public bool isDisabled;
```

### `EnableIFAttribute`

Enables a field in the inspector only if a specified boolean field is `true`.

```csharp
[EnableIF("isEnabled")]
public int myValue;
public bool isEnabled;
```

### `HideIFAttribute`

Hides a field in the inspector if a specified boolean field is `true`.

```csharp
[HideIF("shouldHide")]
public int myValue;
public bool shouldHide;
```

### `ShowIFAttribute`

Shows a field in the inspector only if a specified boolean field is `true`.

```csharp
[ShowIF("shouldShow")]
public int myValue;
public bool shouldShow;
```

### `ReadOnlyAttribute`

Displays a field as read-only in the inspector.

```csharp
[ReadOnly]
public int myValue;
```

### `RequiredAttribute`

Marks a field as required. Shows a help box if the field is empty or null.

```csharp
[Required]
public string myText;

[Required("Custom message")]
public GameObject myObject;
```

### `HelpAttribute`

Displays a help box above a field in the inspector.

```csharp
[Help("This is an info message.", HelpMessageType.Info)]
public int myValue;
```

### `SeparatorAttribute`

Draws a separator line with an optional label to organize fields in the inspector.

```csharp
[Separator("Section Title")]
public int myValue;
```

## Installation

### Option 1: Unity Git Package Manager (Recommended)

Add the following line to your project's `manifest.json` dependencies:

```json
"com.ran-utils.custom-attribute": "https://github.com/khalishzhafran/utils-custom-attribute.git"
```

This will automatically fetch and update the package via Unity's Package Manager.

### Option 2: Manual Copy

Copy the `Editor/Attributes` folder into your Unity project's `Assets` directory.

## Usage

Simply add the desired attribute to your MonoBehaviour or ScriptableObject fields. The custom drawers will automatically enhance the inspector UI in the Unity Editor.

## License

Copyright (c) 2025 Ran. Free to use, modify, and distribute for personal and commercial projects as long as this notice remains intact.
