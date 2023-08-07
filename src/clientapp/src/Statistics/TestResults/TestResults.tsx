import Display from "../../common/components/Display/Display";
import { Table } from "react-bootstrap";
import { useEffect, useState } from "react";
import { getTests, TestResponse } from "../../common/services/test-service";
import TestResult from "./TestResult/TestResult";

export default function TestResults() {
  const [testResults, setTestResults] = useState<TestResponse[] | null>(null);

  useEffect(() => {
    getTests().then(setTestResults);
  }, []);

  if (testResults === null) {
    return (
      <div className="text-center fs-4">
        {"Loading..."}
      </div>
    );
  }

  if (testResults.length === 0) {
    return (
      <div className="text-center fs-4">
        You don't have any translations. Press "import" to add new
      </div>
    );
  }

  return (
    <>
      <Display size={3} text="Test results" className="text-center mb-5"/>
      <div className="table-responsive">
        <Table className="translation-table align-middle">
          <tbody id="translations">
          {testResults.map((r, i) => <TestResult key={i} testResult={r}/>)}
          </tbody>
        </Table>
      </div>
    </>);

};
