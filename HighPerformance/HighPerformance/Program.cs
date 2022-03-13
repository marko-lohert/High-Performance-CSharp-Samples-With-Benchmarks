using BenchmarkDotNet.Running;
using HighPerformance.Miscellaneous;
using HighPerformance.Span;
using HighPerformance.Array;

//BenchmarkRunner.Run<SpanVsSubstring>();
//BenchmarkRunner.Run<ClassVsStructVsRecordVsRecordStruct>();
BenchmarkRunner.Run<StackAllocVsRegularHeapAlloc_WithInit>();

