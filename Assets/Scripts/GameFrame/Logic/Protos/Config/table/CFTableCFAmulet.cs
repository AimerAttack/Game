// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: table/CFTableCFAmulet.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Proto.Config.Table {

  /// <summary>Holder for reflection information generated from table/CFTableCFAmulet.proto</summary>
  public static partial class CFTableCFAmuletReflection {

    #region Descriptor
    /// <summary>File descriptor for table/CFTableCFAmulet.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static CFTableCFAmuletReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cht0YWJsZS9DRlRhYmxlQ0ZBbXVsZXQucHJvdG8SElByb3RvLkNvbmZpZy5U",
            "YWJsZRocY29tbW9uL0NGQmFzZVRhYmxlSW5mby5wcm90bxoVZGVmaW5lL0NG",
            "QW11bGV0LnByb3RvItYBCg9DRlRhYmxlQ0ZBbXVsZXQSPQoFZGF0YXMYASAD",
            "KAsyLi5Qcm90by5Db25maWcuVGFibGUuQ0ZUYWJsZUNGQW11bGV0LkRhdGFz",
            "RW50cnkSNwoJdGFibGVJbmZvGAIgASgLMiQuUHJvdG8uQ29uZmlnLkNvbW1v",
            "bi5DRkJhc2VUYWJsZUluZm8aSwoKRGF0YXNFbnRyeRILCgNrZXkYASABKBES",
            "LAoFdmFsdWUYAiABKAsyHS5Qcm90by5Db25maWcuRGVmaW5lLkNGQW11bGV0",
            "OgI4AWIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Proto.Config.Common.CFBaseTableInfoReflection.Descriptor, global::Proto.Config.Define.CFAmuletReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Proto.Config.Table.CFTableCFAmulet), global::Proto.Config.Table.CFTableCFAmulet.Parser, new[]{ "Datas", "TableInfo" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { null, })
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class CFTableCFAmulet : pb::IMessage<CFTableCFAmulet>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<CFTableCFAmulet> _parser = new pb::MessageParser<CFTableCFAmulet>(() => new CFTableCFAmulet());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<CFTableCFAmulet> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Proto.Config.Table.CFTableCFAmuletReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CFTableCFAmulet() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CFTableCFAmulet(CFTableCFAmulet other) : this() {
      datas_ = other.datas_.Clone();
      tableInfo_ = other.tableInfo_ != null ? other.tableInfo_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CFTableCFAmulet Clone() {
      return new CFTableCFAmulet(this);
    }

    /// <summary>Field number for the "datas" field.</summary>
    public const int DatasFieldNumber = 1;
    private static readonly pbc::MapField<int, global::Proto.Config.Define.CFAmulet>.Codec _map_datas_codec
        = new pbc::MapField<int, global::Proto.Config.Define.CFAmulet>.Codec(pb::FieldCodec.ForSInt32(8, 0), pb::FieldCodec.ForMessage(18, global::Proto.Config.Define.CFAmulet.Parser), 10);
    private readonly pbc::MapField<int, global::Proto.Config.Define.CFAmulet> datas_ = new pbc::MapField<int, global::Proto.Config.Define.CFAmulet>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::MapField<int, global::Proto.Config.Define.CFAmulet> Datas {
      get { return datas_; }
    }

    /// <summary>Field number for the "tableInfo" field.</summary>
    public const int TableInfoFieldNumber = 2;
    private global::Proto.Config.Common.CFBaseTableInfo tableInfo_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Proto.Config.Common.CFBaseTableInfo TableInfo {
      get { return tableInfo_; }
      set {
        tableInfo_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as CFTableCFAmulet);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(CFTableCFAmulet other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!Datas.Equals(other.Datas)) return false;
      if (!object.Equals(TableInfo, other.TableInfo)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= Datas.GetHashCode();
      if (tableInfo_ != null) hash ^= TableInfo.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      datas_.WriteTo(output, _map_datas_codec);
      if (tableInfo_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(TableInfo);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      datas_.WriteTo(ref output, _map_datas_codec);
      if (tableInfo_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(TableInfo);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      size += datas_.CalculateSize(_map_datas_codec);
      if (tableInfo_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(TableInfo);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(CFTableCFAmulet other) {
      if (other == null) {
        return;
      }
      datas_.Add(other.datas_);
      if (other.tableInfo_ != null) {
        if (tableInfo_ == null) {
          TableInfo = new global::Proto.Config.Common.CFBaseTableInfo();
        }
        TableInfo.MergeFrom(other.TableInfo);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            datas_.AddEntriesFrom(input, _map_datas_codec);
            break;
          }
          case 18: {
            if (tableInfo_ == null) {
              TableInfo = new global::Proto.Config.Common.CFBaseTableInfo();
            }
            input.ReadMessage(TableInfo);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            datas_.AddEntriesFrom(ref input, _map_datas_codec);
            break;
          }
          case 18: {
            if (tableInfo_ == null) {
              TableInfo = new global::Proto.Config.Common.CFBaseTableInfo();
            }
            input.ReadMessage(TableInfo);
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
