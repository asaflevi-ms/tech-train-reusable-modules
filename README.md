# tech-train-reusable-modules
API Extensible Library for API Validation 

## Getting started
* Clone this repo and get [common project] (https://github.com/asaflevi-ms/tech-train-reusable-modules/tree/main/src/common)

* Add the project to your solution.
* Add common as project reference to your process/webapp 

## Register Validators
```c#
builder.Services.AddApiValidator(configuration)
    .AddApiValidator<T1>()
    .AddApiValidator<T2>();
where T1/T2... are IApiValidator
```

#### Examples for Known Validator types
* GroupCollectionValidator
* ControllerValidator
* ApiPathValidator 

## register validator middleware 
```c#
var app = builder.Build();
app.UseMiddleware<ApiValidatorMiddleware>();
```

## register event logger validator callback 

Implement your Event logger class
```c#
	public class ValidationEventLogger 
	{
		private readonly IApiValidatorManager _apiValidatorManager;
		public ValidationEventLogger(IApiValidatorManager apiValidatorManager)
		{
			_apiValidatorManager = apiValidatorManager;
      // 
      // Register Event callback
			_apiValidatorManager.OnApiValidatorEvent += ProcessEventFunc;
		}

		private void ProcessEventFunc(object? sender, ApiValidatorEventArgs e)
		{
			// Log the event
			Console.WriteLine(JsonSerializer.Serialize(e.errorMessage));
		}
	}
```

Add the event logger as singleton
Program.cs
```c#
builder.Services.AddSingleton<ValidationEventLogger>();


var app = builder.Build();
app.Services.GetRequiredService<ValidationEventLogger>();
```



### Register ApiValidatorMiddleware
```c#
var app = builder.Build();
app.UseMiddleware<ApiValidatorMiddleware>();
```




## Extend and implement your own validator
'''C#
public class MyValidator : IApiValidator
	{
		public string Kind => "MyValidator";

		public ValidationError? Validate(dynamic value)
		{
			if (value is <validationType> myType)
			{
			  /* 
         * Check validation and return error(s)
         * Multiple errors can be set in ValidationError.Details
         * Inner error should be set in ValidationError.InnerError
         */
				return new ValidationError(Kind,....);
			}

			return null;
		}
	}
'''

And in Program.cs register your new validator
```c#
    builder.Services.AddApiValidator(configuration)
    .AddApiValidator<MyValidator>()
```