import { GeneratorConfig } from 'ng-openapi';

const config: GeneratorConfig = {
  input: './openapi.json',
  output: './src/client',
  options: {
    dateType: 'Date',
    enumStyle: 'enum'
  }
};

export default config;