<script lang="ts">
  let { className = "", value = $bindable(), ...restProps }: {
    className?: string;
    value: number;
    [key: string]: unknown;
  } = $props();
</script>

<input {...restProps}
       bind:value
       class="input text-center p-1 {className}"
       type="number"
/>
