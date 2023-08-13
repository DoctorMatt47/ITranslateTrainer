[assembly: StronglyTypedIdDefaults(
    backingType: StronglyTypedIdBackingType.Int,
    converters: StronglyTypedIdConverter.TypeConverter
    | StronglyTypedIdConverter.SystemTextJson
    | StronglyTypedIdConverter.EfCoreValueConverter)]
