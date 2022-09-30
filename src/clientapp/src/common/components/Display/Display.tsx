interface DisplayProps {
  size: number;
  text: string;
}

const Display = (props: DisplayProps) => <div
  className={"display-" + props.size}>{props.text}</div>

export default Display;
