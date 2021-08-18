

shell_path=$(pwd)

cd "${shell_path}"

output_dir="${shell_path}/../Assets/Scripts/GameFrame/Logic/Protos/cs"
proto_files=`find "src" -name "*.proto"`

rm -rf "${output_dir}"
mkdir -p "${output_dir}"

#compile
protoc --proto_path="src" --csharp_out="${output_dir}" $proto_files
