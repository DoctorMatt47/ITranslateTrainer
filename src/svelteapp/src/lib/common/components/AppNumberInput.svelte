<script lang="ts">
  export let value1: number;
  export let value2: number;
  export let className = "";
</script>

<input {...$$restProps}
       bind:value1
       class="input text-center p-1 {className}"
       type="number"
/>
<input {...$$restProps}
       bind:value2
       class="input text-center p-1 {className}"
       type="number"
/>
