# imgidx

A simple command line tool to generate a CSV index file from a series of images in the current directory.

The program was built for a specific use case, take a directory with hundreds of files of the format:
```
[number] [description text with spaces].[jpeg|jpg|etc]
```
and output a CSV index in the format:
Index | Details
--- | ---
[number] | [description text with spaces]
... | ...

# History
## Version 0.0.1
Initial commit, generates index as described. No command line options, configurable filename patterns, or exif information capured. Perhaps something for the future if required.

# License
Copyright 2016 Dr Jon Nicholson <[http://www.drjonnicholson.com](www.drjonnicholson.com)>

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.