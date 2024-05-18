<script lang="ts">
  let { className = "", ...restProps }: {
    className?: string;
    [key: string]: unknown;
  } = $props();
</script>

<input {...restProps}
       class="input text-center p-1 {className}"
       type="number"
/>
