﻿using BenchmarkDotNet.Running;
using HighPerformance.Array;
using HighPerformance.Loop;
using HighPerformance.Miscellaneous;
using HighPerformance.SIMD;
using HighPerformance.Span;
using HighPerformance.String;

//BenchmarkRunner.Run<SpanVsSubstring>();
//BenchmarkRunner.Run<ClassVsStructVsRecordVsRecordStruct>();
//BenchmarkRunner.Run<StackAllocVsRegularHeapAlloc_WithInit>();
//BenchmarkRunner.Run<IsPrefixIncrementFasterThanPostfix>();
//BenchmarkRunner.Run<ArrayPoolVsRegularArrayAllocation>();
//BenchmarkRunner.Run<IfVsSwitch>();
//BenchmarkRunner.Run<MemoryLocality>();
//BenchmarkRunner.Run<IntParseVsIntTryParse>();
//BenchmarkRunner.Run<SIMDSumVsForSum>();
//BenchmarkRunner.Run<ForeachVsFor>();
//BenchmarkRunner.Run<SkipLocalsInit>();
//BenchmarkRunner.Run<StringBuilderVsStringConcatenation>();
//BenchmarkRunner.Run<DefineListCapacity>();
//BenchmarkRunner.Run<ParallelForVsRegularFor_MediumSizeContentInsideLoop>();
BenchmarkRunner.Run<ParallelForVsRegularFor_SmallSizeContentInsideLoop>();
