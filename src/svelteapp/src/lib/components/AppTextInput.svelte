<script lang="ts">
  export let value: string;
  export let className = "";

</script>

<input {...$$restProps}
       bind:value
       class="input text-center p-1 dark:bg-black/0 placeholder:text-surface-600 {className}"
       type="text"
/>
