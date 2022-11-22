import Display from "../../common/components/Display/Display";
import {Col, Container, Row, Table} from "react-bootstrap";
import {useEffect, useState} from "react";
import {
  DayResultResponse,
  getDayResults
} from "../../common/services/day-results-service";
import {getTests, TestResponse} from "../../common/services/test-service";
import TestResult from "./TestResult/TestResult";

const TestResults = () => {
  const [testResults, setTestResults] = useState<TestResponse[] | null>(null);
  useEffect(() => {
      getTests()
        .then(t => setTestResults(t))
    },
    []);

  if (testResults === null)
    return (
      <div className="text-center fs-4">
        {"Loading..."}
      </div>
    );

  if (testResults.length === 0)
    return (
      <div className="text-center fs-4">
        You don't have any translations. Press "import" to add new
      </div>
    );

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

export default TestResults;
